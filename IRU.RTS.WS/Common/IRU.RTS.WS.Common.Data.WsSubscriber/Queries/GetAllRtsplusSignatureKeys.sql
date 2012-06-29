IF (@OnlyActive = 0) SET @OnlyActive = null

SELECT
	rsk.[SUBSCRIBER_ID]
	,rsk.[VALID_FROM]
	,rsk.[VALID_TO]
	,rsk.[THUMBPRINT]
	,rsk.[CERT_BLOB]
	,rsk.[PRIVATE_KEY]
	,rsk.[KEY_ACTIVE]	
	,rsk.[CREATION_USERID]
	,rsk.[CREATION_DATETIME]	
	,rsk.[LAST_UPDATE_USERID]
	,rsk.[LAST_UPDATE_DATETIME]
FROM
	[dbo].[RTSPLUS_SIGNATURE_KEYS] rsk with(nolock)
WHERE
	rsk.[KEY_ACTIVE] = isnull(@OnlyActive, rsk.[KEY_ACTIVE])	
ORDER BY
	rsk.[SUBSCRIBER_ID],
	rsk.[VALID_TO] DESC