select 
	wss.SUBSCRIBER_ID
	, wss.SERVICE_ID
	, wss.ACTIVE
	, wssm.METHOD_ID
	, wssm.ACTIVE as [METHOD_ACTIVE]
from
	dbo.WS_SUBSCRIBER_SERVICES wss with(nolock)
inner join	
	dbo.WS_SUBSCRIBER_SERVICE_METHOD wssm with(nolock) on wssm.SERVICE_ID=wss.SERVICE_ID and wssm.SUBSCRIBER_ID=wss.SUBSCRIBER_ID
where 
	wssm.SUBSCRIBER_ID = @SubscriberId and
	wssm.SERVICE_ID = @ServiceId