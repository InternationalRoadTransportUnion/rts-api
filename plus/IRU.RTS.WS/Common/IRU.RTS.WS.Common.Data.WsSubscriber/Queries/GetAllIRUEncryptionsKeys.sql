IF (@OnlyActive = 0) SET @OnlyActive = null

SELECT 
	iek.[ENCRYPTION_KEY_ID]
	,iek.[MODULUS]
	,iek.[EXPONENT]
	,iek.[D]
	,iek.[P]
	,iek.[Q]
	,iek.[DP]
	,iek.[DQ]
	,iek.[INVERSEQ]
	,iek.[DISTRIBUTED_TO]
	,iek.[DISTRIBUTION_DATE]
	,iek.[KEY_ACTIVE]
	,iek.[KEY_ACTIVE_REASON]
	,iek.[CERT_IS_CURRENT]
	,iek.[CERT_EXPIRY_DATE]
	,iek.[CERT_GENERATION_TIME]
	,iek.[CERT_BLOB]
	,iek.[LAST_UPDATE_USERID]
	,iek.[LAST_UPDATE_TIME]
FROM 
	[dbo].[IRU_ENCRYPTION_KEYS] iek with(nolock)
WHERE
	iek.[KEY_ACTIVE] = isnull(@OnlyActive, iek.[KEY_ACTIVE])
ORDER BY
	iek.[DISTRIBUTED_TO], 
	iek.[CERT_EXPIRY_DATE] desc	