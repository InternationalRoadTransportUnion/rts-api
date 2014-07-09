IF (@OnlyActive = 0) SET @OnlyActive = null

SELECT 
	sek.[SUBSCRIBER_ID]
	,sek.[ENCRYPTION_KEY_ID]
	,sek.[MODULUS]
	,sek.[EXPONENT]
	,sek.[CERT_BLOB]
	,sek.[CERT_RECEIVED_DATE]
	,sek.[CERT_RECEIVED_USERID]
	,sek.[CERT_EXPIRY_DATE]
	,sek.[KEY_ACTIVE]
	,sek.[KEY_ACTIVE_REASON]
	,sek.[LAST_UPDATE_USERID]
	,sek.[LAST_UPDATE_TIME]
FROM 
	[dbo].[SUBSCRIBER_ENCRYPTION_KEYS] sek with(nolock)
WHERE
	sek.[KEY_ACTIVE] = isnull(@OnlyActive, sek.[KEY_ACTIVE])
ORDER BY
	sek.[SUBSCRIBER_ID], 
	sek.[CERT_EXPIRY_DATE] desc