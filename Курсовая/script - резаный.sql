USE [SystemO]
GO
/****** Object:  Table [dbo].[AttendanceLog]    Script Date: 24.11.2023 7:13:10 ******/

CREATE TABLE [dbo].[AttendanceLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoomId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[MarkerId] [int] NOT NULL,
	[Date] [date] NOT NULL,
 CONSTRAINT [PK_AttendanceLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Markers]    Script Date: 24.11.2023 7:13:11 ******/

CREATE TABLE [dbo].[Markers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Char] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Markers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 24.11.2023 7:13:11 ******/

CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 24.11.2023 7:13:11 ******/

CREATE TABLE [dbo].[Rooms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 24.11.2023 7:13:11 ******/

CREATE TABLE [dbo].[Students](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NULL,
	[IdRoom] [int] NOT NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 24.11.2023 7:13:11 ******/

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FIO] [nvarchar](150) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AttendanceLog] ON 

INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1002, 1, 1, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1003, 1, 2, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1004, 1, 3, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1005, 1, 4, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1006, 1, 5, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1007, 1, 6, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1008, 2, 7, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1009, 2, 8, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1010, 3, 9, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1011, 3, 10, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1012, 3, 11, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1013, 3, 12, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1014, 3, 13, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1015, 4, 14, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1016, 4, 15, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1017, 4, 16, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1018, 4, 17, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1019, 5, 18, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1020, 5, 19, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1021, 6, 20, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1022, 6, 21, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1023, 6, 22, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1024, 6, 23, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1025, 7, 24, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1026, 7, 25, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1027, 7, 26, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1028, 7, 27, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1029, 8, 28, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1030, 8, 29, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1031, 8, 30, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1032, 8, 31, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1033, 9, 32, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1034, 9, 33, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1035, 9, 34, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1036, 9, 35, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1037, 10, 36, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1038, 10, 37, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1039, 10, 38, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1040, 10, 39, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1041, 11, 40, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1042, 11, 41, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1043, 11, 42, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1044, 12, 43, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1045, 12, 44, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1046, 12, 45, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1047, 13, 46, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1048, 13, 47, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1049, 13, 48, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1050, 14, 49, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1051, 14, 50, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1052, 14, 51, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1053, 15, 52, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1054, 15, 53, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1055, 15, 54, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1056, 16, 55, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1057, 16, 56, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1058, 17, 57, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1059, 17, 58, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1060, 17, 59, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1061, 18, 60, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1062, 18, 61, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1063, 19, 62, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1064, 19, 63, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1065, 19, 64, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1066, 20, 65, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1067, 20, 66, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1068, 20, 67, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1069, 20, 68, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1070, 21, 69, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1071, 21, 70, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1072, 21, 71, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1073, 21, 72, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1074, 22, 73, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1075, 22, 74, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1076, 22, 75, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1077, 22, 76, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1078, 22, 77, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1079, 23, 78, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1080, 23, 79, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1081, 23, 80, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1082, 24, 81, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1083, 24, 82, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1084, 25, 83, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1085, 25, 84, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1086, 25, 85, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1087, 25, 86, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1088, 26, 87, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1089, 26, 88, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1090, 26, 89, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1091, 26, 90, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1092, 27, 91, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1093, 27, 92, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1094, 27, 93, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1095, 27, 94, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1096, 28, 95, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1097, 28, 96, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1098, 28, 97, 1, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1099, 1, 1, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1100, 1, 2, 1, CAST(N'2023-11-18' AS Date))
GO
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1101, 1, 3, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1102, 1, 4, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1103, 1, 5, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1104, 1, 6, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1105, 2, 7, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1106, 2, 8, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1107, 3, 9, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1108, 3, 10, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1109, 3, 11, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1110, 3, 12, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1111, 3, 13, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1112, 4, 14, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1113, 4, 15, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1114, 4, 16, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1115, 4, 17, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1116, 5, 18, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1117, 5, 19, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1118, 6, 20, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1119, 6, 21, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1120, 6, 22, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1121, 6, 23, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1122, 7, 24, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1123, 7, 25, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1124, 7, 26, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1125, 7, 27, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1126, 8, 28, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1127, 8, 29, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1128, 8, 30, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1129, 8, 31, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1130, 9, 32, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1131, 9, 33, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1132, 9, 34, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1133, 9, 35, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1134, 10, 36, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1135, 10, 37, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1136, 10, 38, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1137, 10, 39, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1138, 11, 40, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1139, 11, 41, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1140, 11, 42, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1141, 12, 43, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1142, 12, 44, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1143, 12, 45, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1144, 13, 46, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1145, 13, 47, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1146, 13, 48, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1147, 14, 49, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1148, 14, 50, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1149, 14, 51, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1150, 15, 52, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1151, 15, 53, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1152, 15, 54, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1153, 16, 55, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1154, 16, 56, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1155, 17, 57, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1156, 17, 58, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1157, 17, 59, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1158, 18, 60, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1159, 18, 61, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1160, 19, 62, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1161, 19, 63, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1162, 19, 64, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1163, 20, 65, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1164, 20, 66, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1165, 20, 67, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1166, 20, 68, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1167, 21, 69, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1168, 21, 70, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1169, 21, 71, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1170, 21, 72, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1171, 22, 73, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1172, 22, 74, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1173, 22, 75, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1174, 22, 76, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1175, 22, 77, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1176, 23, 78, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1177, 23, 79, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1178, 23, 80, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1179, 24, 81, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1180, 24, 82, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1181, 25, 83, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1182, 25, 84, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1183, 25, 85, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1184, 25, 86, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1185, 26, 87, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1186, 26, 88, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1187, 26, 89, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1188, 26, 90, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1189, 27, 91, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1190, 27, 92, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1191, 27, 93, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1192, 27, 94, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1193, 28, 95, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1194, 28, 96, 1, CAST(N'2023-11-18' AS Date))
INSERT [dbo].[AttendanceLog] ([Id], [RoomId], [StudentId], [MarkerId], [Date]) VALUES (1195, 28, 97, 1, CAST(N'2023-11-18' AS Date))
SET IDENTITY_INSERT [dbo].[AttendanceLog] OFF
GO
SET IDENTITY_INSERT [dbo].[Markers] ON 

INSERT [dbo].[Markers] ([Id], [Char], [Description]) VALUES (1, N'+', N'Проживающий присутствовал')
INSERT [dbo].[Markers] ([Id], [Char], [Description]) VALUES (2, N'о', N'Проживающий опоздал')
INSERT [dbo].[Markers] ([Id], [Char], [Description]) VALUES (3, N'н', N'Проживающий отсутствовал')
INSERT [dbo].[Markers] ([Id], [Char], [Description]) VALUES (4, N'з', N'Проживающий отсутствовал по заявлению')
INSERT [dbo].[Markers] ([Id], [Char], [Description]) VALUES (5, N'', N'Пустое значение')
SET IDENTITY_INSERT [dbo].[Markers] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Name]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (2, N'User')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (3, N'Guest')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Rooms] ON 

INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (1, N'303')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (2, N'304')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (3, N'305')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (4, N'306')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (5, N'307')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (6, N'308')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (7, N'309')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (8, N'310')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (9, N'311')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (10, N'312')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (11, N'313')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (12, N'314')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (13, N'315')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (14, N'316')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (15, N'317')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (16, N'318')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (17, N'319')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (18, N'320')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (19, N'321')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (20, N'322')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (21, N'323')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (22, N'324')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (23, N'325')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (24, N'326')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (25, N'327')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (26, N'328')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (27, N'329')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (28, N'330')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (29, N'331')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (30, N'332')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (31, N'333')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (32, N'334')
INSERT [dbo].[Rooms] ([Id], [Number]) VALUES (33, N'335')
SET IDENTITY_INSERT [dbo].[Rooms] OFF
GO
SET IDENTITY_INSERT [dbo].[Students] ON 

INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (1, N'Константин', N'Майер', N'Ивана', 1)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (2, N'Евгений', N'Колбас', N'Ивана', 1)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (3, N'Алексей', N'Зуев', N'Ивана', 1)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (4, N'Андрей', N'Ярцев', N'Ивана', 1)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (5, N'Алесандр', N'Коновалов', N'Ивана', 1)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (6, N'Даниил', N'Халин', N'Ивана', 1)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (7, N'Максим', N'Нейфельд', N'Ивана', 2)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (8, N'Данил', N'Подгорбунский', N'Ивана', 2)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (9, N'Виктор', N'Мясников', N'Ивана', 3)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (10, N'Николай', N'Смолкин', N'Ивана', 3)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (11, N'Илья', N'Погребников', N'Ивана', 3)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (12, N'Кирилл', N'Усольцев', N'Ивана', 3)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (13, N'Александр', N'Кротов', N'Ивана', 3)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (14, N'Антон', N'Арлахов', N'Ивана', 4)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (15, N'Михаил', N'Павленко', N'Ивана', 4)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (16, N'Семен', N'Надоля', N'Ивана', 4)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (17, N'Шахриёр', N'Шерализода', N'Ивана', 4)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (18, N'Полина', N'Гужавина', N'Ивана', 5)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (19, N'Роза', N'Чурсина', N'Ивана', 5)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (20, N'Валерия', N'Ядыкина', N'Ивана', 6)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (21, N'Александра', N'Шаравина', N'Ивана', 6)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (22, N'Евгения', N'Здерева', N'Ивана', 6)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (23, N'Серафима', N'Кондакова', N'Ивана', 6)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (24, N'Тимофей', N'Резвих', N'Ивана', 7)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (25, N'Николай', N'Игнатьев', N'Ивана', 7)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (26, N'Вячеслав', N'Струщенко', N'Ивана', 7)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (27, N'Сергей', N'Глотов', N'Ивана', 7)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (28, N'Анастасия', N'Маскалева', N'Ивана', 8)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (29, N'Светлана', N'Сенченко', N'Ивана', 8)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (30, N'Ангелина', N'Рояк', N'Ивана', 8)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (31, N'Диана', N'Борисова', N'Ивана', 8)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (32, N'Алексей', N'Радионов', N'Ивана', 9)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (33, N'Данил', N'Кондратьев', N'Ивана', 9)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (34, N'Роман', N'Ковалев', N'Ивана', 9)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (35, N'Владислав', N'Тиканов', N'Ивана', 9)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (36, N'Данил', N'Головцов', N'Ивана', 10)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (37, N'Антон', N'Шарапов', N'Ивана', 10)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (38, N'Амыр', N'Ооржак', N'Ивана', 10)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (39, N'Ринат', N'Яныканов', N'Ивана', 10)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (40, N'Мария', N'Богданова', N'Ивана', 11)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (41, N'Николь', N'Воробьева', N'Ивана', 11)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (42, N'Анастасия', N'Захарова', N'Ивана', 11)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (43, N'София', N'Аильчиева', N'Ивана', 12)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (44, N'Арина', N'Авдошина', N'Ивана', 12)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (45, N'Елизавета', N'Архипова', N'Ивана', 12)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (46, N'Дарья', N'Смольникова', N'Ивана', 13)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (47, N'Елизавета', N'Зоткина', N'Ивана', 13)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (48, N'Екатерина', N'Пакушина', N'Ивана', 13)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (49, N'Ангелина', N'Байрамова', N'Ивана', 14)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (50, N'Дарья', N'Корнилова', N'Ивана', 14)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (51, N'Алинда', N'Шанданова', N'Ивана', 14)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (52, N'Кристина', N'Ионова', N'Ивана', 15)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (53, N'Мадина', N'Кропочева', N'Ивана', 15)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (54, N'Надежда', N'Говорина', N'Ивана', 15)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (55, N'Юлия', N'Шерембеева', N'Ивана', 16)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (56, N'Замира', N'Масловская', N'Ивана', 16)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (57, N'Анастасия', N'Деткова', N'Ивана', 17)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (58, N'Александра', N'Киселёва', N'Ивана', 17)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (59, N'Наталья', N'Цокова', N'Ивана', 17)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (60, N'Неля', N'Габова', N'Ивана', 18)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (61, N'Юлия', N'Саламова', N'Ивана', 18)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (62, N'Маргарита', N'Кратюк', N'Ивана', 19)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (63, N'Кристина', N'Иванова', N'Ивана', 19)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (64, N'Маргарита', N'Калугина', N'Ивана', 19)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (65, N'Яна', N'Саблина', N'Ивана', 20)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (66, N'Анна', N'Самойлова', N'Ивана', 20)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (67, N'Марина', N'Тохнина', N'Ивана', 20)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (68, N'Ирина', N'Мосина', N'Ивана', 20)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (69, N'Валерия', N'Шевцова', N'Ивана', 21)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (70, N'Асем', N'Бутабай', N'Ивана', 21)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (71, N'Анастасия', N'Красноперова', N'Ивана', 21)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (72, N'Дарья', N'Карманова', N'Ивана', 21)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (73, N'Анастасия', N'Логвиненко', N'Ивана', 22)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (74, N'Анастасия', N'Паутова', N'Ивана', 22)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (75, N'Анастасия', N'Логинова', N'Ивана', 22)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (76, N'Василина', N'Рябцева', N'Ивана', 22)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (77, N'Руслана', N'Феделеш', N'Ивана', 22)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (78, N'Виолетта', N'Олейник', N'Ивана', 23)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (79, N'Виктория', N'Шапорева', N'Ивана', 23)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (80, N'Валентина', N'Соломина', N'Ивана', 23)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (81, N'Яна', N'Стребкова', N'Ивана', 24)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (82, N'Анна', N'Шаталова', N'Ивана', 24)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (83, N'Дарина', N'Опарина', N'Ивана', 25)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (84, N'Елизавета', N'Куянова', N'Ивана', 25)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (85, N'Виктория', N'Садова', N'Ивана', 25)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (86, N'Ульяна', N'Брютова', N'Ивана', 25)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (87, N'Юлия', N'Продьма', N'Ивана', 26)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (88, N'Златислава', N'Горбунова', N'Ивана', 26)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (89, N'Дарья', N'Ламанова', N'Ивана', 26)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (90, N'Алиса', N'Данишевская', N'Ивана', 26)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (91, N'Лия', N'Мельникова', N'Ивана', 27)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (92, N'Валерия', N'Потапова', N'Ивана', 27)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (93, N'Ксения', N'Фазулова', N'Ивана', 27)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (94, N'Арина', N'Корпейко', N'Ивана', 27)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (95, N'Ирина', N'Манулиц', N'Ивана', 28)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (96, N'Ульяна', N'Серебренникова', N'Ивана', 28)
INSERT [dbo].[Students] ([Id], [Name], [Surname], [Patronymic], [IdRoom]) VALUES (97, N'Ксения', N'Слащёва', N'Ивана', 28)
SET IDENTITY_INSERT [dbo].[Students] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [FIO], [Login], [Password], [RoleId]) VALUES (1, N'Струщенко Вячелав Павлович', N'is2011_svp', N'123', 1)
INSERT [dbo].[Users] ([Id], [FIO], [Login], [Password], [RoleId]) VALUES (2, N'Игнатьев Николай Дмитреевич', N'kola', N'123', 2)
INSERT [dbo].[Users] ([Id], [FIO], [Login], [Password], [RoleId]) VALUES (3, N'Guest', N'', N'', 3)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[AttendanceLog]  WITH CHECK ADD  CONSTRAINT [FK_AttendanceLog_Markers] FOREIGN KEY([MarkerId])
REFERENCES [dbo].[Markers] ([Id])
GO
ALTER TABLE [dbo].[AttendanceLog] CHECK CONSTRAINT [FK_AttendanceLog_Markers]
GO
ALTER TABLE [dbo].[AttendanceLog]  WITH CHECK ADD  CONSTRAINT [FK_AttendanceLog_Rooms] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([Id])
GO
ALTER TABLE [dbo].[AttendanceLog] CHECK CONSTRAINT [FK_AttendanceLog_Rooms]
GO
ALTER TABLE [dbo].[AttendanceLog]  WITH CHECK ADD  CONSTRAINT [FK_AttendanceLog_Students] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([Id])
GO
ALTER TABLE [dbo].[AttendanceLog] CHECK CONSTRAINT [FK_AttendanceLog_Students]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Журнал посещаемости' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AttendanceLog'
GO
