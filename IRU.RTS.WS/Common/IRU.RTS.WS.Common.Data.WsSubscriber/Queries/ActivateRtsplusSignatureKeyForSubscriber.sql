SET NOCOUNT OFF

BEGIN TRY
	DECLARE @RowCount int
	
	UPDATE dbo.RTSPLUS_SIGNATURE_KEYS
	SET
		KEY_ACTIVE = 1
		,LAST_UPDATE_USERID = SUSER_SNAME()
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
	ELSE
	BEGIN
		UPDATE dbo.RTSPLUS_SIGNATURE_KEYS
		SET
			KEY_ACTIVE = 0
			,LAST_UPDATE_USERID = SUSER_SNAME()
			,LAST_UPDATE_DATETIME = getDate()
		WHERE
			(
				(@ServerCertificate = 0 AND PRIVATE_KEY IS NULL)
				OR
				(@ServerCertificate = 1 AND PRIVATE_KEY IS NOT NULL)
			)
			AND SUBSCRIBER_ID = @SubscriberId
			AND THUMBPRINT <> @Thumbprint	
	END
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