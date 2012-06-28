SELECT
	rsk.[SUBSCRIBER_ID]
	,rsk.[VALID_FROM]
	,rsk.[VALID_TO]
	,rsk.[THUMBPRINT]
	,rsk.[CERT_BLOB]
	,rsk.[KEY_USAGE]
	,rsk.[PRIVATE_KEY]
	,rsk.[LAST_UPDATE_USERID]
	,rsk.[LAST_UPDATE_DATETIME]
FROM
	[dbo].[RTSPLUS_SIGNATURE_KEYS] rsk with(nolock)