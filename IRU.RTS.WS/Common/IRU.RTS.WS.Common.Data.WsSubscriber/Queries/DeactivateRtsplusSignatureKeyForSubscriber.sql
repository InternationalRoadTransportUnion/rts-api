SET NOCOUNT OFF

BEGIN TRY
	DECLARE @RowCount int
	
	IF (@UserId is null)
		SET @UserId = SUSER_SNAME()
	
	UPDATE dbo.RTSPLUS_SIGNATURE_KEYS
	SET
		KEY_ACTIVE = 0
		,LAST_UPDATE_USERID = @UserId
		,LAST_UPDATE_DATETIME = getDate()
	WHERE
		(
			(@ServerCertificate = 0 AND PRIVATE_KEY IS NULL)
			OR
			(@ServerCertificate = 1 AND PRIVATE_KEY IS NOT NULL)
		)
		AND SUBSCRIBER_ID = @SubscriberId
		AND THUMBPRINT = @Thumbprint
		
	SET @RowCount = @@ROWCOUNT
	
	IF (@RowCount = 0)
		RAISERROR (N'No record updated', 10, 1)
END TRY
BEGIN CATCH
DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;

    SELECT 
        @ErrorMessage = ERROR_MESSAGE(),
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE();

    RAISERROR (@ErrorMessage,
               @ErrorSeverity,
               @ErrorState);
END CATCH