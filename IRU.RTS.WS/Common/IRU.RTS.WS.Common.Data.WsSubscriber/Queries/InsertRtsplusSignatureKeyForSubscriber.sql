BEGIN TRY
	DECLARE @UserId NVARCHAR(50)
	DECLARE @Now DATETIME
	
	SET @UserId = SUSER_SNAME()
	SET @Now = getDate()

	IF NOT EXISTS (SELECT 1 FROM dbo.RTSPLUS_SIGNATURE_KEYS WHERE SUBSCRIBER_ID=@SubscriberId AND CERT_BLOB=@CertBlob AND ((PRIVATE_KEY IS NOT NULL AND @PrivateKey IS NOT NULL) OR (PRIVATE_KEY IS NULL AND @PrivateKey IS NULL)))
	BEGIN
		INSERT INTO dbo.RTSPLUS_SIGNATURE_KEYS
			(SUBSCRIBER_ID, VALID_FROM, VALID_TO, THUMBPRINT, CERT_BLOB, PRIVATE_KEY, KEY_ACTIVE, CREATION_USERID, CREATION_DATETIME, LAST_UPDATE_USERID, LAST_UPDATE_DATETIME)
		VALUES
			(@SubscriberId, @ValidFrom, @ValidTo, @Thumbprint, @CertBlob, @PrivateKey, @KeyActive, @UserId, @Now, @UserId, @Now)
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