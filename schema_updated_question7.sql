USE [JAG2019]

GO
/****** Object:  Table [dbo].[Lead]    Script Date: 2018/11/12 4:48:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lead](
	[LeadId] [bigint]  IDENTITY(1,1 )NOT NULL,
	[TrackingCode] [varchar](20) NOT NULL,
	[FirstName] [nvarchar](255) NOT NULL,
	[LastName] [nvarchar](255) NOT NULL,
	[ContactNumber] [varchar](20) NOT NULL,
	[Email] [varchar](150) NULL,
	[ReceivedDateTime] [datetime] NOT NULL,
	[IPAddress] [varchar](50) NULL,
	[UserAgent] [varchar](500) NULL,
	[ReferrerURL] [varchar](500) NULL,
	[IsDuplicate] [bit] NULL,
	[IsCapped] [bit] NULL,
	[IsSuccessful] [bit] NULL,
 CONSTRAINT [PK_Lead] PRIMARY KEY CLUSTERED 
(
	[LeadId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeadParameter]    Script Date: 2018/11/12 4:48:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeadParameter](
	[LeadParameterId] [bigint] IDENTITY(1,1) NOT NULL,
	[LeadId] [bigint] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Value] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_LeadParameter] PRIMARY KEY CLUSTERED 
(
	[LeadParameterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LeadParameter]  WITH CHECK ADD  CONSTRAINT [FK_LeadParameter_Lead] FOREIGN KEY([LeadId])
REFERENCES [dbo].[Lead] ([LeadId])
GO
ALTER TABLE [dbo].[LeadParameter] CHECK CONSTRAINT [FK_LeadParameter_Lead]
GO
