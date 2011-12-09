select 
	sek.SUBSCRIBER_ID
	, sek.ENCRYPTION_KEY_ID
	, sek.CERT_EXPIRY_DATE
	, sek.KEY_ACTIVE 
from dbo.SUBSCRIBER_ENCRYPTION_KEYS sek with(nolock)
where 
	sek.SUBSCRIBER_ID = @SubscriberId
order by 
	sek.KEY_ACTIVE desc, sek.CERT_EXPIRY_DATE desc