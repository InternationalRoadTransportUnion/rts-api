-- if the carnet has been invalidated by the IRU (Status 3)
IF (EXISTS (SELECT Top 1 1 FROM dbo.CAR_CUR_STOPPED_CARNETS with (nolock) WHERE S_C_CARNET_NUMBER = @TIRNumber)) 
BEGIN 
	SELECT 
		@TIRNumber AS TIRNumber,
		3 AS RESULT,
		null AS I_HolderID,
		null AS ExpiryDate, 
		null AS AssociationShortName,
		(SELECT C_G_GUARANTEE_NUMBER FROM dbo.CAR_CUR_ADDITIONAL_GUARANTEE_TABLE with (nolock) WHERE C_G_CARNET_NUMBER = @TIRNumber AND @WithVoucherNumber = 1) AS VoucherNumber
END 
ELSE 
BEGIN 
	-- if the carnet number does not exist (Status 5)
	IF (NOT EXISTS (SELECT Top 1 1 FROM dbo.CAR_CUR_CARNET_TABLE with (nolock) WHERE C_C_CARNET_NUMBER = @TIRNumber)) 	 
	BEGIN
		SELECT  
			@TIRNumber AS TIRNumber,
			5 AS RESULT,
			null AS I_HolderID,
			null AS ExpiryDate, 
			null AS AssociationShortName,
			(SELECT C_G_GUARANTEE_NUMBER FROM dbo.CAR_CUR_ADDITIONAL_GUARANTEE_TABLE with (nolock) WHERE C_G_CARNET_NUMBER = @TIRNumber AND @WithVoucherNumber = 1) AS VoucherNumber
	END  
	ELSE  
	BEGIN  
		IF((SELECT C_C_STATE FROM dbo.CAR_CUR_CARNET_TABLE with (nolock) WHERE C_C_CARNET_NUMBER = @TIRNumber) IN (0,95,5,9,101)) 
		BEGIN 
			-- if the carnet has been issued (Status 1, issue data available)
			IF ((SELECT C_I_ISSUE_DATE FROM dbo.CAR_CUR_ISSUE_TABLE with (nolock) WHERE C_I_CARNET_NUMBER = @TIRNumber) is not null) 
			BEGIN 
				SELECT CAR_CUR_ISSUE_TABLE.C_I_CARNET_NUMBER as TIRNumber, 1 AS RESULT,  
				CAR_CUR_ISSUE_TABLE.C_I_HOLDER as I_HolderID,  
				CAR_CUR_ISSUE_TABLE.C_I_EXP_DATE as ExpiryDate,  
				CAR_CUR_SYS_ASSOC_DETAILS.C_XST_ASSOCIATION_TXT as AssociationShortName,
				CAR_CUR_ADDITIONAL_GUARANTEE_TABLE.C_G_GUARANTEE_NUMBER as VoucherNumber
				FROM dbo.CAR_CUR_ISSUE_TABLE with (nolock)
				INNER JOIN dbo.CAR_CUR_SYS_ASSOC_DETAILS with (nolock) ON CAR_CUR_ISSUE_TABLE.C_I_ASSOCIATION = CAR_CUR_SYS_ASSOC_DETAILS.C_XST_ASSOCIATION
				LEFT JOIN dbo.CAR_CUR_ADDITIONAL_GUARANTEE_TABLE with (nolock) ON (CAR_CUR_ADDITIONAL_GUARANTEE_TABLE.C_G_CARNET_NUMBER = CAR_CUR_ISSUE_TABLE.C_I_CARNET_NUMBER AND @WithVoucherNumber = 1)
				WHERE (CAR_CUR_ISSUE_TABLE.C_I_CARNET_NUMBER = @TIRNumber)  
			END 
			ELSE 
			BEGIN 
				-- the carnet has not been issued (Status 2, no issue data available)
				IF(( SELECT C_P_FACT_BULLETIN_DATE FROM dbo.CAR_CUR_PACKET_TABLE with (nolock) WHERE C_P_FACT_PKT_CARNET_NUMBER=(((CONVERT(INT,(@TIRNumber-1)/50))*50)+1) ) is not null) 
				BEGIN  
					IF (SELECT C_C_STATE FROM dbo.CAR_CUR_CARNET_TABLE with (nolock) WHERE C_C_CARNET_NUMBER=@TIRNumber) <> 0
					BEGIN
						SELECT CAR_CUR_CARNET_TABLE.C_C_CARNET_NUMBER as TIRNumber, 2 AS RESULT,  
							CAR_CUR_SYS_ASSOC_DETAILS.C_XST_ASSOCIATION_TXT as AssociationShortName,
							CAR_CUR_ADDITIONAL_GUARANTEE_TABLE.C_G_GUARANTEE_NUMBER as VoucherNumber
						FROM dbo.CAR_CUR_CARNET_TABLE with (nolock)
						LEFT OUTER JOIN dbo.CAR_CUR_SYS_ASSOC_DETAILS with (nolock) ON CAR_CUR_CARNET_TABLE.C_C_ASSOCIATION = CAR_CUR_SYS_ASSOC_DETAILS.C_XST_ASSOCIATION
						LEFT JOIN dbo.CAR_CUR_ADDITIONAL_GUARANTEE_TABLE with (nolock) ON (CAR_CUR_ADDITIONAL_GUARANTEE_TABLE.C_G_CARNET_NUMBER = CAR_CUR_CARNET_TABLE.C_C_CARNET_NUMBER AND @WithVoucherNumber = 1)
						WHERE (CAR_CUR_CARNET_TABLE.C_C_CARNET_NUMBER = @TIRNumber)  
					END
					ELSE
					BEGIN
						SELECT CAR_CUR_CARNET_TABLE.C_C_CARNET_NUMBER as TIRNumber, 2 AS RESULT,  
							CAR_CUR_SYS_ASSOC_DETAILS.C_XST_ASSOCIATION_TXT as AssociationShortName,
							CAR_CUR_ADDITIONAL_GUARANTEE_TABLE.C_G_GUARANTEE_NUMBER as VoucherNumber
						FROM dbo.CAR_CUR_CARNET_TABLE with (nolock)
						LEFT OUTER JOIN dbo.CAR_CUR_PACKET_TABLE with (nolock) ON C_P_FACT_PKT_CARNET_NUMBER=(((CONVERT(INT,(@TIRNumber-1)/50))*50)+1) 
						LEFT OUTER JOIN dbo.CAR_CUR_SYS_ASSOC_DETAILS with (nolock) ON CAR_CUR_PACKET_TABLE.C_P_FACT_ASSOCIATION = CAR_CUR_SYS_ASSOC_DETAILS.C_XST_ASSOCIATION  
						LEFT JOIN dbo.CAR_CUR_ADDITIONAL_GUARANTEE_TABLE with (nolock) ON (CAR_CUR_ADDITIONAL_GUARANTEE_TABLE.C_G_CARNET_NUMBER = CAR_CUR_CARNET_TABLE.C_C_CARNET_NUMBER AND @WithVoucherNumber = 1)
						WHERE (CAR_CUR_CARNET_TABLE.C_C_CARNET_NUMBER = @TIRNumber)  
					END
				END 
				ELSE -- the carnet is not in circulation
				BEGIN 
					SELECT  
						@TIRNumber AS TIRNumber,
						4 AS RESULT,
						null AS I_HolderID,
						null AS ExpiryDate, 
						null AS AssociationShortName,
						(SELECT C_G_GUARANTEE_NUMBER FROM dbo.CAR_CUR_ADDITIONAL_GUARANTEE_TABLE with (nolock) WHERE C_G_CARNET_NUMBER = @TIRNumber AND @WithVoucherNumber = 1) AS VoucherNumber
				END 
			END  
		END  
		ELSE -- the carnet is not in circulation (Status 4)
		BEGIN  
			SELECT  
				@TIRNumber AS TIRNumber,
				4 AS RESULT,
				null AS I_HolderID,
				null AS ExpiryDate, 
				null AS AssociationShortName,
				(SELECT C_G_GUARANTEE_NUMBER FROM dbo.CAR_CUR_ADDITIONAL_GUARANTEE_TABLE with (nolock) WHERE C_G_CARNET_NUMBER = @TIRNumber AND @WithVoucherNumber = 1) AS VoucherNumber  
		END 
	END 
END