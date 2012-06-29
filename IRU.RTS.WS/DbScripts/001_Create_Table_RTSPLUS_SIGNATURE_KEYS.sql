﻿USE [WS_SUBSCRIBER_DB]
GO

/****** Object:  Table [dbo].[RTSPLUS_SIGNATURE_KEYS]    Script Date: 06/25/2012 16:07:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RTSPLUS_SIGNATURE_KEYS]') AND type in (N'U'))
DROP TABLE [dbo].[RTSPLUS_SIGNATURE_KEYS]
GO

/****** Object:  Table [dbo].[RTSPLUS_SIGNATURE_KEYS]    Script Date: 06/25/2012 16:07:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RTSPLUS_SIGNATURE_KEYS](
	[SUBSCRIBER_ID] [nvarchar](255) NOT NULL,
	[VALID_FROM] [datetime] NOT NULL,
	[VALID_TO] [datetime] NOT NULL,
	[THUMBPRINT] [varchar](128) NOT NULL,
	[CERT_BLOB] [binary](4096) NOT NULL,
	[PRIVATE_KEY] [xml] NULL,
	[KEY_ACTIVE] [bit] NOT NULL,
	[CREATION_USERID] [nvarchar](50) NOT NULL,
	[CREATION_DATETIME] [datetime] NOT NULL,
	[LAST_UPDATE_USERID] [nvarchar](50) NOT NULL,
	[LAST_UPDATE_DATETIME] [datetime] NOT NULL
) ON [PRIMARY]

GO


/****** Object:  Index [IDX_RTSPLUS_SIGNATURE_KEYS_SUBSCRIBER_ID]    Script Date: 06/25/2012 16:08:39 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RTSPLUS_SIGNATURE_KEYS]') AND name = N'IDX_RTSPLUS_SIGNATURE_KEYS_SUBSCRIBER_ID')
DROP INDEX [IDX_RTSPLUS_SIGNATURE_KEYS_SUBSCRIBER_ID] ON [dbo].[RTSPLUS_SIGNATURE_KEYS] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [IDX_RTSPLUS_SIGNATURE_KEYS_SUBSCRIBER_ID] ON [dbo].[RTSPLUS_SIGNATURE_KEYS] 
(
	[SUBSCRIBER_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

/****** Object:  Index [IDX_RTSPLUS_SIGNATURE_KEYS_VALID_FROM_VALID_TO]    Script Date: 06/25/2012 16:09:01 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RTSPLUS_SIGNATURE_KEYS]') AND name = N'IDX_RTSPLUS_SIGNATURE_KEYS_VALID_FROM_VALID_TO')
DROP INDEX [IDX_RTSPLUS_SIGNATURE_KEYS_VALID_FROM_VALID_TO] ON [dbo].[RTSPLUS_SIGNATURE_KEYS] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [IDX_RTSPLUS_SIGNATURE_KEYS_VALID_FROM_VALID_TO] ON [dbo].[RTSPLUS_SIGNATURE_KEYS] 
(
	[VALID_FROM] ASC,
	[VALID_TO] DESC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

/****** Object:  Index [IDX_RTSPLUS_SIGNATURE_KEYS_KEY_ACTIVE]    Script Date: 06/29/2012 15:43:00 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RTSPLUS_SIGNATURE_KEYS]') AND name = N'IDX_RTSPLUS_SIGNATURE_KEYS_KEY_ACTIVE')
DROP INDEX [IDX_RTSPLUS_SIGNATURE_KEYS_KEY_ACTIVE] ON [dbo].[RTSPLUS_SIGNATURE_KEYS] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [IDX_RTSPLUS_SIGNATURE_KEYS_KEY_ACTIVE] ON [dbo].[RTSPLUS_SIGNATURE_KEYS] 
(
	[KEY_ACTIVE] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


SET ANSI_PADDING OFF
GO


