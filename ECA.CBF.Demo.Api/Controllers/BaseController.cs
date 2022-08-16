using ECA.CBF.Demo.Entities.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ECA.CBF.Demo.Api.Controllers;

public class BaseController : ControllerBase
{
    protected readonly ILogger<BaseController> Logger;

    public BaseController(ILogger<BaseController> logger)
    {
        Logger = logger;
    }

    protected string GetRoute()
    {
        IHttpRequestFeature feature = HttpContext.Features.Get<IHttpRequestFeature>();
        return feature.RawTarget;
    }

    protected async Task<IActionResult> PostDataAsync<TEntity, TResult, TConflictException>(
        Func<Task<TResult>> operationExecutorMethod,
        TEntity dataInput)
        where TConflictException : InternalErrorException
    {
        if (dataInput == null)
        {
            var ex = (TConflictException)Activator.CreateInstance(typeof(TConflictException), Array.Empty<object>());
            return BadRequest(ex.Message);
        }
        return await ExecuteAsync(async () =>
        {
            try
            {
                TResult result = await operationExecutorMethod();
                return Created(GetRoute(), result);
            }
            catch (ResourceNotFoundException e)
            {
                return ResourceNotFound(e);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        });
    }
    protected async Task<IActionResult> PutDataAsync<TEntity, TConflictException>(
        Func<Task> operationExecutorMethod,
        TEntity dataInput)
        where TConflictException : InternalErrorException
    {
        if (dataInput == null)
        {
            var ex = (TConflictException)Activator.CreateInstance(typeof(TConflictException), Array.Empty<object>());
            return BadRequest(ex.Message);
        }
        return await ExecuteAsync(async () =>
        {
            try
            {
                await operationExecutorMethod();
                return Ok();
            }
            catch (ResourceNotFoundException e)
            {
                return ResourceNotFound(e);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        });
    }

    protected async Task<IActionResult> GetListAsync<TResult>(Func<Task<TResult>> operationExecutorMethod)
    {
        return await ExecuteAsync(async () =>
        {
            try
            {
                TResult result = await operationExecutorMethod();
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (ResourceNotFoundException e)
            {
                return ResourceNotFound(e);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        });
    }

    protected async Task<IActionResult> ExecuteAsync(Func<Task<IActionResult>> metodoExecucao)
    {
        try
        {
            return await metodoExecucao();
        }
        catch (Exception e)
        {
            Logger.LogError($"Error on: {GetRoute()}. Menssage: {e.Message} StackTrace: {e.StackTrace}");
            return InternalServerError(e);
        }
    }

    protected IActionResult InternalServerError(Exception e)
    {
        Logger.LogError($"Error on: {GetRoute()}. Menssage: {e.Message} StackTrace: {e.StackTrace}");
        return StatusCode((int)HttpStatusCode.InternalServerError, e);
    }

    protected IActionResult ResourceNotFound(ResourceNotFoundException e)
    {
        Logger.LogError($"Error on: {GetRoute()}. Resource not found! Menssage: {e.Message} StackTrace: {e.StackTrace}");
        return StatusCode(StatusCodes.Status404NotFound, e.Message);
    }
}