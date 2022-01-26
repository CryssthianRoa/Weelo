USE [Weelo_Database]
GO
SET IDENTITY_INSERT [Weelo].[Owner] ON 
GO
INSERT [Weelo].[Owner] ([IdOwner], [Name], [Address], [Photo], [Birthday]) VALUES (1, N'Cryssthian Roa', N'Carrera 8', N'photo.jpg', CAST(N'2022-01-20' AS Date))
GO
INSERT [Weelo].[Owner] ([IdOwner], [Name], [Address], [Photo], [Birthday]) VALUES (2, N'Carlos Marin', N'Calle 12', N'photo.jpg', CAST(N'2022-01-26' AS Date))
GO
SET IDENTITY_INSERT [Weelo].[Owner] OFF
GO
SET IDENTITY_INSERT [Weelo].[Property] ON 
GO
INSERT [Weelo].[Property] ([IdProperty], [IdOwner], [Name], [Address], [Price], [CodeInternal], [Year]) VALUES (1, 2, N'Apto 302', N'Calle 80', CAST(8000000.00000000 AS Numeric(19, 8)), N'APT302-UNKN-CRYS', 2008)
GO
INSERT [Weelo].[Property] ([IdProperty], [IdOwner], [Name], [Address], [Price], [CodeInternal], [Year]) VALUES (7, 2, N'Apto 414 Torre 5 Gerona', N'Calle 140', CAST(1700000.00000000 AS Numeric(19, 8)), N'APT414-GERO-CRYS', 1996)
GO
INSERT [Weelo].[Property] ([IdProperty], [IdOwner], [Name], [Address], [Price], [CodeInternal], [Year]) VALUES (9, 1, N'Apto 304', N'Calle 80', CAST(7000000.00000000 AS Numeric(19, 8)), N'APT304-UNKN-CRYS', 2018)
GO
INSERT [Weelo].[Property] ([IdProperty], [IdOwner], [Name], [Address], [Price], [CodeInternal], [Year]) VALUES (10, 1, N'Apto 306', N'Calle 80', CAST(8000000.00000000 AS Numeric(19, 8)), N'APT306-UNKN-CRYS', 2017)
GO
INSERT [Weelo].[Property] ([IdProperty], [IdOwner], [Name], [Address], [Price], [CodeInternal], [Year]) VALUES (11, 2, N'Apto 307', N'Calle 80', CAST(8000000.00000000 AS Numeric(19, 8)), N'APT307-UNKN-CRYS', 2017)
GO
SET IDENTITY_INSERT [Weelo].[Property] OFF
GO
SET IDENTITY_INSERT [Weelo].[PropertyImage] ON 
GO
INSERT [Weelo].[PropertyImage] ([IdPropertyImage], [IdProperty], [File], [Enabled]) VALUES (13, 1, N'1apto.jpg', 1)
GO
INSERT [Weelo].[PropertyImage] ([IdPropertyImage], [IdProperty], [File], [Enabled]) VALUES (14, 7, N'2apto.jpg', 1)
GO
INSERT [Weelo].[PropertyImage] ([IdPropertyImage], [IdProperty], [File], [Enabled]) VALUES (15, 9, N'3apto.jpg', 1)
GO
INSERT [Weelo].[PropertyImage] ([IdPropertyImage], [IdProperty], [File], [Enabled]) VALUES (16, 10, N'4apto.jpg', 1)
GO
INSERT [Weelo].[PropertyImage] ([IdPropertyImage], [IdProperty], [File], [Enabled]) VALUES (17, 11, N'5apto.jpg', 1)
GO
INSERT [Weelo].[PropertyImage] ([IdPropertyImage], [IdProperty], [File], [Enabled]) VALUES (18, 1, N'2apto.jpg', 1)
GO
INSERT [Weelo].[PropertyImage] ([IdPropertyImage], [IdProperty], [File], [Enabled]) VALUES (19, 1, N'6apto.jpg', 1)
GO
SET IDENTITY_INSERT [Weelo].[PropertyImage] OFF
GO
