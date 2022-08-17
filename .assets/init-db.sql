USE [master]
GO

/****** Object:  Database [cbf-demo-api]    Script Date: 17/08/2022 20:30:57 ******/
CREATE DATABASE [cbf-demo-api]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'cbf-demo-api', FILENAME = N'/var/opt/mssql/data/cbf-demo-api.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'cbf-demo-api_log', FILENAME = N'/var/opt/mssql/data/cbf-demo-api_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [cbf-demo-api].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [cbf-demo-api] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [cbf-demo-api] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [cbf-demo-api] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [cbf-demo-api] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [cbf-demo-api] SET ARITHABORT OFF 
GO

ALTER DATABASE [cbf-demo-api] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [cbf-demo-api] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [cbf-demo-api] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [cbf-demo-api] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [cbf-demo-api] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [cbf-demo-api] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [cbf-demo-api] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [cbf-demo-api] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [cbf-demo-api] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [cbf-demo-api] SET  DISABLE_BROKER 
GO

ALTER DATABASE [cbf-demo-api] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [cbf-demo-api] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [cbf-demo-api] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [cbf-demo-api] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [cbf-demo-api] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [cbf-demo-api] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [cbf-demo-api] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [cbf-demo-api] SET RECOVERY FULL 
GO

ALTER DATABASE [cbf-demo-api] SET  MULTI_USER 
GO

ALTER DATABASE [cbf-demo-api] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [cbf-demo-api] SET DB_CHAINING OFF 
GO

ALTER DATABASE [cbf-demo-api] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [cbf-demo-api] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [cbf-demo-api] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [cbf-demo-api] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [cbf-demo-api] SET QUERY_STORE = OFF
GO

ALTER DATABASE [cbf-demo-api] SET  READ_WRITE 
GO

BEGIN TRAN

USE [cbf-demo-api]
GO
/****** Object:  Table [dbo].[card]    Script Date: 17/08/2022 20:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[card](
	[card_id] [int] IDENTITY(1,1) NOT NULL,
	[card_type] [nchar](1) NOT NULL,
	[card_description] [nvarchar](100) NOT NULL,
	[card_match_id] [int] NOT NULL,
	[card_player_id] [int] NOT NULL,
	[card_team_id] [int] NOT NULL,
	[card_date_time] [datetime] NOT NULL,
 CONSTRAINT [PK_card] PRIMARY KEY CLUSTERED 
(
	[card_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[goal]    Script Date: 17/08/2022 20:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[goal](
	[goal_id] [int] IDENTITY(1,1) NOT NULL,
	[goal_match_id] [int] NOT NULL,
	[goal_player_id] [int] NOT NULL,
	[goal_team_id] [int] NOT NULL,
	[goal_date_time] [datetime] NOT NULL,
 CONSTRAINT [PK_goal] PRIMARY KEY CLUSTERED 
(
	[goal_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[league]    Script Date: 17/08/2022 20:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[league](
	[league_id] [int] IDENTITY(1,1) NOT NULL,
	[league_name] [nvarchar](2) NOT NULL,
	[league_description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_league] PRIMARY KEY CLUSTERED 
(
	[league_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[match]    Script Date: 17/08/2022 20:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[match](
	[match_id] [int] IDENTITY(1,1) NOT NULL,
	[match_stadium] [nvarchar](50) NOT NULL,
	[match_dt_provided] [datetime] NOT NULL,
	[match_dt_start] [datetime] NULL,
	[match_dt_start_break] [datetime] NULL,
	[match_dt_end_break] [datetime] NULL,
	[match_dt_end] [datetime] NULL,
	[match_tournament_id] [int] NOT NULL,
	[match_team_host] [int] NOT NULL,
	[match_team_guest] [int] NOT NULL,
	[match_referee] [int] NULL,
 CONSTRAINT [PK_match] PRIMARY KEY CLUSTERED 
(
	[match_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[player]    Script Date: 17/08/2022 20:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[player](
	[player_id] [int] IDENTITY(1,1) NOT NULL,
	[player_name] [nvarchar](50) NOT NULL,
	[player_dt_birth] [datetime] NOT NULL,
	[player_country] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_player] PRIMARY KEY CLUSTERED 
(
	[player_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[replacement]    Script Date: 17/08/2022 20:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[replacement](
	[replacement_id] [int] IDENTITY(1,1) NOT NULL,
	[replacement_match_id] [int] NOT NULL,
	[replacement_player_in_id] [int] NOT NULL,
	[replacement_player_out_id] [int] NOT NULL,
	[replacement_team_id] [int] NOT NULL,
	[replacement_date_time] [datetime] NOT NULL,
 CONSTRAINT [PK_replacement] PRIMARY KEY CLUSTERED 
(
	[replacement_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[team]    Script Date: 17/08/2022 20:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[team](
	[team_id] [int] IDENTITY(1,1) NOT NULL,
	[team_name] [nvarchar](50) NOT NULL,
	[team_short_name] [nvarchar](3) NOT NULL,
	[team_city] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_team] PRIMARY KEY CLUSTERED 
(
	[team_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tournament]    Script Date: 17/08/2022 20:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tournament](
	[tournament_id] [int] IDENTITY(1,1) NOT NULL,
	[tournament_name] [nvarchar](50) NOT NULL,
	[tournament_grade] [nvarchar](50) NOT NULL,
	[tournament_season] [int] NOT NULL,
	[tournament_league_id] [int] NOT NULL,
 CONSTRAINT [PK_tournament] PRIMARY KEY CLUSTERED 
(
	[tournament_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[transfer]    Script Date: 17/08/2022 20:30:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transfer](
	[transfer_id] [int] IDENTITY(1,1) NOT NULL,
	[transfer_type] [nvarchar](10) NOT NULL,
	[transfer_value] [int] NOT NULL,
	[transfer_observation] [nvarchar](100) NOT NULL,
	[transfer_player_id] [int] NOT NULL,
	[transfer_team_in_id] [int] NOT NULL,
	[transfer_team_out_id] [int] NOT NULL,
	[transfer_date_time] [datetime] NOT NULL,
 CONSTRAINT [PK_transfer] PRIMARY KEY CLUSTERED 
(
	[transfer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[card] ON 
GO
INSERT [dbo].[card] ([card_id], [card_type], [card_description], [card_match_id], [card_player_id], [card_team_id], [card_date_time]) VALUES (1, N'A', N'Cartão amarelo por falta na área', 6, 1, 4, CAST(N'2022-08-15T22:36:24.407' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[card] OFF
GO
SET IDENTITY_INSERT [dbo].[goal] ON 
GO
INSERT [dbo].[goal] ([goal_id], [goal_match_id], [goal_player_id], [goal_team_id], [goal_date_time]) VALUES (1, 6, 1, 1, CAST(N'2022-08-15T21:13:36.440' AS DateTime))
GO
INSERT [dbo].[goal] ([goal_id], [goal_match_id], [goal_player_id], [goal_team_id], [goal_date_time]) VALUES (2, 6, 3, 4, CAST(N'2022-08-15T22:02:36.223' AS DateTime))
GO
INSERT [dbo].[goal] ([goal_id], [goal_match_id], [goal_player_id], [goal_team_id], [goal_date_time]) VALUES (3, 6, 3, 4, CAST(N'2022-08-15T22:23:36.223' AS DateTime))
GO
INSERT [dbo].[goal] ([goal_id], [goal_match_id], [goal_player_id], [goal_team_id], [goal_date_time]) VALUES (5, 8, 9, 3, CAST(N'2022-08-16T12:32:03.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[goal] OFF
GO
SET IDENTITY_INSERT [dbo].[league] ON 
GO
INSERT [dbo].[league] ([league_id], [league_name], [league_description]) VALUES (1, N'BA', N'Brasileiro Série A')
GO
INSERT [dbo].[league] ([league_id], [league_name], [league_description]) VALUES (2, N'BB', N'Brasileiro Série B')
GO
INSERT [dbo].[league] ([league_id], [league_name], [league_description]) VALUES (3, N'BC', N'Brasileiro Série C')
GO
INSERT [dbo].[league] ([league_id], [league_name], [league_description]) VALUES (4, N'CB', N'Copa do Brasil')
GO
SET IDENTITY_INSERT [dbo].[league] OFF
GO
SET IDENTITY_INSERT [dbo].[match] ON 
GO
INSERT [dbo].[match] ([match_id], [match_stadium], [match_dt_provided], [match_dt_start], [match_dt_start_break], [match_dt_end_break], [match_dt_end], [match_tournament_id], [match_team_host], [match_team_guest], [match_referee]) VALUES (6, N'Mineirão', CAST(N'2022-07-16T16:00:00.000' AS DateTime), NULL, CAST(N'2022-01-01T23:58:10.000' AS DateTime), CAST(N'2022-01-02T23:58:10.000' AS DateTime), CAST(N'2022-07-16T16:05:00.000' AS DateTime), 1, 1, 4, 1)
GO
INSERT [dbo].[match] ([match_id], [match_stadium], [match_dt_provided], [match_dt_start], [match_dt_start_break], [match_dt_end_break], [match_dt_end], [match_tournament_id], [match_team_host], [match_team_guest], [match_referee]) VALUES (7, N'Maracanã', CAST(N'2022-08-16T00:44:02.993' AS DateTime), CAST(N'2020-01-01T00:00:00.000' AS DateTime), NULL, NULL, NULL, 1, 3, 4, 4)
GO
INSERT [dbo].[match] ([match_id], [match_stadium], [match_dt_provided], [match_dt_start], [match_dt_start_break], [match_dt_end_break], [match_dt_end], [match_tournament_id], [match_team_host], [match_team_guest], [match_referee]) VALUES (8, N'Industrial CL', CAST(N'2022-08-10T12:00:00.000' AS DateTime), CAST(N'1973-09-26T18:41:29.187' AS DateTime), NULL, NULL, CAST(N'1973-09-26T18:41:29.187' AS DateTime), 2, 3, 4, 6)
GO
INSERT [dbo].[match] ([match_id], [match_stadium], [match_dt_provided], [match_dt_start], [match_dt_start_break], [match_dt_end_break], [match_dt_end], [match_tournament_id], [match_team_host], [match_team_guest], [match_referee]) VALUES (9, N'La Bombonera', CAST(N'2022-08-17T00:37:10.763' AS DateTime), NULL, NULL, NULL, NULL, 1, 3, 4, 9)
GO
SET IDENTITY_INSERT [dbo].[match] OFF
GO
SET IDENTITY_INSERT [dbo].[player] ON 
GO
INSERT [dbo].[player] ([player_id], [player_name], [player_dt_birth], [player_country]) VALUES (1, N'Zé Vicente', CAST(N'1999-01-01T00:00:00.000' AS DateTime), N'Brasil')
GO
INSERT [dbo].[player] ([player_id], [player_name], [player_dt_birth], [player_country]) VALUES (3, N'José Brito', CAST(N'2001-01-23T00:00:00.000' AS DateTime), N'Argentina')
GO
INSERT [dbo].[player] ([player_id], [player_name], [player_dt_birth], [player_country]) VALUES (4, N'Amarildo Junqueira', CAST(N'1999-01-01T00:00:00.000' AS DateTime), N'Brasil')
GO
INSERT [dbo].[player] ([player_id], [player_name], [player_dt_birth], [player_country]) VALUES (5, N'Américo Vespúcio', CAST(N'1999-01-01T00:00:00.000' AS DateTime), N'Brasil')
GO
INSERT [dbo].[player] ([player_id], [player_name], [player_dt_birth], [player_country]) VALUES (6, N'José Maurício', CAST(N'1999-01-01T00:00:00.000' AS DateTime), N'Brasil')
GO
INSERT [dbo].[player] ([player_id], [player_name], [player_dt_birth], [player_country]) VALUES (7, N'Alfredo Rabelo', CAST(N'1999-01-01T00:00:00.000' AS DateTime), N'Brasil')
GO
INSERT [dbo].[player] ([player_id], [player_name], [player_dt_birth], [player_country]) VALUES (8, N'Jorge Couto', CAST(N'1999-01-01T00:00:00.000' AS DateTime), N'Brasil')
GO
INSERT [dbo].[player] ([player_id], [player_name], [player_dt_birth], [player_country]) VALUES (9, N'Amilcar Machado', CAST(N'1999-01-01T00:00:00.000' AS DateTime), N'Brasil')
GO
INSERT [dbo].[player] ([player_id], [player_name], [player_dt_birth], [player_country]) VALUES (10, N'José Eustáquio', CAST(N'1999-01-01T00:00:00.000' AS DateTime), N'Brasil')
GO
SET IDENTITY_INSERT [dbo].[player] OFF
GO
SET IDENTITY_INSERT [dbo].[replacement] ON 
GO
INSERT [dbo].[replacement] ([replacement_id], [replacement_match_id], [replacement_player_in_id], [replacement_player_out_id], [replacement_team_id], [replacement_date_time]) VALUES (5, 6, 1, 3, 4, CAST(N'2022-08-15T13:38:15.343' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[replacement] OFF
GO
SET IDENTITY_INSERT [dbo].[team] ON 
GO
INSERT [dbo].[team] ([team_id], [team_name], [team_short_name], [team_city]) VALUES (1, N'Cruzeiro Esporte Clube', N'CFC', N'Belo Horizonte')
GO
INSERT [dbo].[team] ([team_id], [team_name], [team_short_name], [team_city]) VALUES (2, N'Clube Atlético Mineiro', N'CAM', N'Belo Horizonte')
GO
INSERT [dbo].[team] ([team_id], [team_name], [team_short_name], [team_city]) VALUES (3, N'Associação Desportiva Viracopos', N'ADV', N'Conselheiro Lafaiete')
GO
INSERT [dbo].[team] ([team_id], [team_name], [team_short_name], [team_city]) VALUES (4, N'Butina Futebol Clube', N'BFC', N'Conselheiro Lafaiete')
GO
SET IDENTITY_INSERT [dbo].[team] OFF
GO
SET IDENTITY_INSERT [dbo].[tournament] ON 
GO
INSERT [dbo].[tournament] ([tournament_id], [tournament_name], [tournament_grade], [tournament_season], [tournament_league_id]) VALUES (1, N'Campeonato Brasileiro 2022', N'AA', 202200, 1)
GO
INSERT [dbo].[tournament] ([tournament_id], [tournament_name], [tournament_grade], [tournament_season], [tournament_league_id]) VALUES (2, N'Copa do Brasil 2022', N'AA', 202201, 1)
GO
SET IDENTITY_INSERT [dbo].[tournament] OFF
GO
SET IDENTITY_INSERT [dbo].[transfer] ON 
GO
INSERT [dbo].[transfer] ([transfer_id], [transfer_type], [transfer_value], [transfer_observation], [transfer_player_id], [transfer_team_in_id], [transfer_team_out_id], [transfer_date_time]) VALUES (1, N'Venda', 4896, N'Transferencia por negociação', 1, 1, 2, CAST(N'2022-08-14T23:58:03.387' AS DateTime))
GO
INSERT [dbo].[transfer] ([transfer_id], [transfer_type], [transfer_value], [transfer_observation], [transfer_player_id], [transfer_team_in_id], [transfer_team_out_id], [transfer_date_time]) VALUES (2, N'Emprestimo', 25698, N'Nenum', 3, 1, 4, CAST(N'2022-01-15T00:15:09.453' AS DateTime))
GO
INSERT [dbo].[transfer] ([transfer_id], [transfer_type], [transfer_value], [transfer_observation], [transfer_player_id], [transfer_team_in_id], [transfer_team_out_id], [transfer_date_time]) VALUES (3, N'Emprestimo', 25628, N'Nenum', 3, 4, 2, CAST(N'2022-05-15T00:15:09.453' AS DateTime))
GO
INSERT [dbo].[transfer] ([transfer_id], [transfer_type], [transfer_value], [transfer_observation], [transfer_player_id], [transfer_team_in_id], [transfer_team_out_id], [transfer_date_time]) VALUES (7, N'Transfer', 8739, N'Nenhum', 5, 1, 4, CAST(N'2022-08-16T01:03:08.817' AS DateTime))
GO
INSERT [dbo].[transfer] ([transfer_id], [transfer_type], [transfer_value], [transfer_observation], [transfer_player_id], [transfer_team_in_id], [transfer_team_out_id], [transfer_date_time]) VALUES (8, N'Transfer', 8739, N'Nenhum', 6, 1, 4, CAST(N'2022-08-16T01:03:08.817' AS DateTime))
GO
INSERT [dbo].[transfer] ([transfer_id], [transfer_type], [transfer_value], [transfer_observation], [transfer_player_id], [transfer_team_in_id], [transfer_team_out_id], [transfer_date_time]) VALUES (9, N'Transfer', 8739, N'Nenhum', 7, 3, 3, CAST(N'2022-08-16T01:03:08.817' AS DateTime))
GO
INSERT [dbo].[transfer] ([transfer_id], [transfer_type], [transfer_value], [transfer_observation], [transfer_player_id], [transfer_team_in_id], [transfer_team_out_id], [transfer_date_time]) VALUES (10, N'Transfer', 8739, N'Nenhum', 8, 3, 3, CAST(N'2022-08-16T01:03:08.817' AS DateTime))
GO
INSERT [dbo].[transfer] ([transfer_id], [transfer_type], [transfer_value], [transfer_observation], [transfer_player_id], [transfer_team_in_id], [transfer_team_out_id], [transfer_date_time]) VALUES (11, N'Transfer', 8739, N'Nenhum', 9, 3, 3, CAST(N'2022-08-16T01:03:08.817' AS DateTime))
GO
INSERT [dbo].[transfer] ([transfer_id], [transfer_type], [transfer_value], [transfer_observation], [transfer_player_id], [transfer_team_in_id], [transfer_team_out_id], [transfer_date_time]) VALUES (12, N'Transfer', 8739, N'Nenhum', 10, 3, 3, CAST(N'2022-08-16T01:03:08.817' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[transfer] OFF
GO
ALTER TABLE [dbo].[card]  WITH CHECK ADD  CONSTRAINT [FK_card_match] FOREIGN KEY([card_match_id])
REFERENCES [dbo].[match] ([match_id])
GO
ALTER TABLE [dbo].[card] CHECK CONSTRAINT [FK_card_match]
GO
ALTER TABLE [dbo].[card]  WITH CHECK ADD  CONSTRAINT [FK_card_player_in] FOREIGN KEY([card_player_id])
REFERENCES [dbo].[player] ([player_id])
GO
ALTER TABLE [dbo].[card] CHECK CONSTRAINT [FK_card_player_in]
GO
ALTER TABLE [dbo].[card]  WITH CHECK ADD  CONSTRAINT [FK_card_team] FOREIGN KEY([card_team_id])
REFERENCES [dbo].[team] ([team_id])
GO
ALTER TABLE [dbo].[card] CHECK CONSTRAINT [FK_card_team]
GO
ALTER TABLE [dbo].[goal]  WITH CHECK ADD  CONSTRAINT [FK_goal_match] FOREIGN KEY([goal_match_id])
REFERENCES [dbo].[match] ([match_id])
GO
ALTER TABLE [dbo].[goal] CHECK CONSTRAINT [FK_goal_match]
GO
ALTER TABLE [dbo].[goal]  WITH CHECK ADD  CONSTRAINT [FK_goal_player] FOREIGN KEY([goal_player_id])
REFERENCES [dbo].[player] ([player_id])
GO
ALTER TABLE [dbo].[goal] CHECK CONSTRAINT [FK_goal_player]
GO
ALTER TABLE [dbo].[goal]  WITH CHECK ADD  CONSTRAINT [FK_goal_team] FOREIGN KEY([goal_team_id])
REFERENCES [dbo].[team] ([team_id])
GO
ALTER TABLE [dbo].[goal] CHECK CONSTRAINT [FK_goal_team]
GO
ALTER TABLE [dbo].[match]  WITH CHECK ADD  CONSTRAINT [FK_match_referee] FOREIGN KEY([match_referee])
REFERENCES [dbo].[player] ([player_id])
GO
ALTER TABLE [dbo].[match] CHECK CONSTRAINT [FK_match_referee]
GO
ALTER TABLE [dbo].[match]  WITH CHECK ADD  CONSTRAINT [FK_match_team_guest] FOREIGN KEY([match_team_guest])
REFERENCES [dbo].[team] ([team_id])
GO
ALTER TABLE [dbo].[match] CHECK CONSTRAINT [FK_match_team_guest]
GO
ALTER TABLE [dbo].[match]  WITH CHECK ADD  CONSTRAINT [FK_match_team_host] FOREIGN KEY([match_team_host])
REFERENCES [dbo].[team] ([team_id])
GO
ALTER TABLE [dbo].[match] CHECK CONSTRAINT [FK_match_team_host]
GO
ALTER TABLE [dbo].[match]  WITH CHECK ADD  CONSTRAINT [FK_match_tournament] FOREIGN KEY([match_tournament_id])
REFERENCES [dbo].[tournament] ([tournament_id])
GO
ALTER TABLE [dbo].[match] CHECK CONSTRAINT [FK_match_tournament]
GO
ALTER TABLE [dbo].[replacement]  WITH CHECK ADD  CONSTRAINT [FK_replacement_match] FOREIGN KEY([replacement_match_id])
REFERENCES [dbo].[match] ([match_id])
GO
ALTER TABLE [dbo].[replacement] CHECK CONSTRAINT [FK_replacement_match]
GO
ALTER TABLE [dbo].[replacement]  WITH CHECK ADD  CONSTRAINT [FK_replacement_player_in] FOREIGN KEY([replacement_player_in_id])
REFERENCES [dbo].[player] ([player_id])
GO
ALTER TABLE [dbo].[replacement] CHECK CONSTRAINT [FK_replacement_player_in]
GO
ALTER TABLE [dbo].[replacement]  WITH CHECK ADD  CONSTRAINT [FK_replacement_player_out] FOREIGN KEY([replacement_player_out_id])
REFERENCES [dbo].[player] ([player_id])
GO
ALTER TABLE [dbo].[replacement] CHECK CONSTRAINT [FK_replacement_player_out]
GO
ALTER TABLE [dbo].[replacement]  WITH CHECK ADD  CONSTRAINT [FK_replacement_team] FOREIGN KEY([replacement_team_id])
REFERENCES [dbo].[team] ([team_id])
GO
ALTER TABLE [dbo].[replacement] CHECK CONSTRAINT [FK_replacement_team]
GO
ALTER TABLE [dbo].[tournament]  WITH CHECK ADD  CONSTRAINT [FK_tournament] FOREIGN KEY([tournament_league_id])
REFERENCES [dbo].[league] ([league_id])
GO
ALTER TABLE [dbo].[tournament] CHECK CONSTRAINT [FK_tournament]
GO
ALTER TABLE [dbo].[transfer]  WITH CHECK ADD  CONSTRAINT [FK_transfer_player_in] FOREIGN KEY([transfer_player_id])
REFERENCES [dbo].[player] ([player_id])
GO
ALTER TABLE [dbo].[transfer] CHECK CONSTRAINT [FK_transfer_player_in]
GO
ALTER TABLE [dbo].[transfer]  WITH CHECK ADD  CONSTRAINT [FK_transfer_team_in] FOREIGN KEY([transfer_team_in_id])
REFERENCES [dbo].[team] ([team_id])
GO
ALTER TABLE [dbo].[transfer] CHECK CONSTRAINT [FK_transfer_team_in]
GO
ALTER TABLE [dbo].[transfer]  WITH CHECK ADD  CONSTRAINT [FK_transfer_team_out] FOREIGN KEY([transfer_team_out_id])
REFERENCES [dbo].[team] ([team_id])
GO
ALTER TABLE [dbo].[transfer] CHECK CONSTRAINT [FK_transfer_team_out]
GO
COMMIT
