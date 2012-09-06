USE [RTSPLUS_LOG]
GO

/****** Object:  Table [dbo].[WS_CALLS]    Script Date: 09/06/2012 17:33:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WS_CALLS](
	[SubscriberId] [nvarchar](255) NOT NULL,
	[RequestIpAddress] [nvarchar](15) NOT NULL,
	[RequestAction] [nvarchar](255) NULL,
	[RequestContent] [xml] NULL,
	[RequestDateTime] [datetime] NOT NULL,
	[ReplyAction] [nvarchar](255) NULL,
	[ReplyContent] [xml] NULL,
	[ReplyDateTime] [datetime] NULL,
	[CreationDateTime] [datetime] NOT NULL
) ON [PRIMARY]

GO


USE [RTSPLUS_LOG]
/****** Object:  Index [IDX_WS_CALLS_CreationDateTime]    Script Date: 09/06/2012 17:33:35 ******/
CREATE NONCLUSTERED INDEX [IDX_WS_CALLS_CreationDateTime] ON [dbo].[WS_CALLS] 
(
	[CreationDateTime] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


USE [RTSPLUS_LOG]
/****** Object:  Index [IDX_WS_CALLS_RequestAction]    Script Date: 09/06/2012 17:33:35 ******/
CREATE NONCLUSTERED INDEX [IDX_WS_CALLS_RequestAction] ON [dbo].[WS_CALLS] 
(
	[RequestAction] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


USE [RTSPLUS_LOG]
/****** Object:  Index [IDX_WS_CALLS_SubscriberId]    Script Date: 09/06/2012 17:33:35 ******/
CREATE NONCLUSTERED INDEX [IDX_WS_CALLS_SubscriberId] ON [dbo].[WS_CALLS] 
(
	[SubscriberId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[WS_CALLS] ADD  CONSTRAINT [DF_WS_CALLS_SubscriberId]  DEFAULT ('') FOR [SubscriberId]
GO


