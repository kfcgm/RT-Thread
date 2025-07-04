USE [Goods]
GO
/****** Object:  Table [dbo].[Fruit]    Script Date: 2025/6/28 10:17:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fruit](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [char](50) NULL,
	[price] [int] NULL,
	[number] [int] NULL,
	[image] [char](100) NULL,
	[note] [char](100) NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Fruit] ON 

INSERT [dbo].[Fruit] ([id], [name], [price], [number], [image], [note]) VALUES (1, N'apple                                             ', 2, 86, N'C:\Images\apple.jpg                                                                                 ', N'apple                                                                                               ')
INSERT [dbo].[Fruit] ([id], [name], [price], [number], [image], [note]) VALUES (2, N'banana                                            ', 4, 82, N'C:\Images\banana.jpg                                                                                ', N'banana                                                                                              ')
INSERT [dbo].[Fruit] ([id], [name], [price], [number], [image], [note]) VALUES (3, N'orange                                            ', 6, 5, N'C:\Images\orange.jpg                                                                                ', N'orange                                                                                              ')
INSERT [dbo].[Fruit] ([id], [name], [price], [number], [image], [note]) VALUES (4, N'grape                                             ', 5, 10, N'C:\Images\grape.jpg                                                                                 ', N'grape                                                                                               ')
INSERT [dbo].[Fruit] ([id], [name], [price], [number], [image], [note]) VALUES (5, N'pear                                              ', 5, 21, N'C:\Images\pear.jpg                                                                                  ', N'pear                                                                                                ')
INSERT [dbo].[Fruit] ([id], [name], [price], [number], [image], [note]) VALUES (1004, N'cookie                                            ', 6, 143, N'C:\Images\cookie.jpg                                                                                ', N'cookie                                                                                              ')
SET IDENTITY_INSERT [dbo].[Fruit] OFF
GO
