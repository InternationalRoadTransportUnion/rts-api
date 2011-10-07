-- if the carnet has been invalidated by the IRU (Status 3)
IF (EXISTS (SELECT Top 1 1 FROM CAR_CUR_STOPPED_CARNETS with (nolock) WHERE S_C_CARNET_NUMBER = @TIRNumber)) 
BEGIN 
	SELECT @TIRNumber AS TIRNumber, 3 AS RESULT  
END 
ELSE 
BEGIN 
	-- if the carnet number does not exist (Status 5)
	IF (NOT EXISTS (SELECT Top 1 1 FROM CAR_CUR_CARNET_TABLE with (nolock) WHERE C_C_CARNET_NUMBER = @TIRNumber)) 	 
	BEGIN  
		SELECT @TIRNumber AS TIRNumber, 5 AS RESULT	
	END  
	ELSE  
	BEGIN  
		IF((SELECT C_C_STATE FROM CAR_CUR_CARNET_TABLE with (nolock) WHERE C_C_CARNET_NUMBER = @TIRNumber) IN (0,95,5,9)) 
		BEGIN 
			-- if the carnet has been issued (Status 1, issue data available)
			IF ((SELECT C_I_ISSUE_DATE FROM CAR_CUR_ISSUE_TABLE with (nolock) WHERE C_I_CARNET_NUMBER = @TIRNumber) is not null) 
			BEGIN 
				SELECT CAR_CUR_ISSUE_TABLE.C_I_CARNET_NUMBER as TIRNumber, 1 AS RESULT,  
				CAR_CUR_ISSUE_TABLE.C_I_HOLDER as I_HolderID,  
				CAR_CUR_ISSUE_TABLE.C_I_EXP_DATE as ExpiryDate,  
				CAR_CUR_SYS_ASSOC_DETAILS.C_XST_ASSOCIATION_TXT as AssociationShortName,  
				(SELECT COUNT(*) FROM Dispatch.dbo.CAR_DISP_CARNETS with (nolock) WHERE (Dispatch.dbo.CAR_DISP_CARNETS.D_C_CARNET_NUMBER = @TIRNumber)) AS NoOfDischarges 
				FROM CAR_CUR_ISSUE_TABLE with (nolock) INNER JOIN  
				CAR_CUR_SYS_ASSOC_DETAILS with (nolock) ON CAR_CUR_ISSUE_TABLE.C_I_ASSOCIATION = CAR_CUR_SYS_ASSOC_DETAILS.C_XST_ASSOCIATION  
				WHERE (CAR_CUR_ISSUE_TABLE.C_I_CARNET_NUMBER = @TIRNumber)  
			END 
			ELSE 
			BEGIN 
				-- the carnet has not been issued (Status 2, no issue data available)
				IF(( SELECT C_P_FACT_BULLETIN_DATE FROM CAR_CUR_PACKET_TABLE with (nolock) WHERE C_P_FACT_PKT_CARNET_NUMBER=(((CONVERT(INT,(@TIRNumber-1)/50))*50)+1) ) is not null) 
				BEGIN  
					IF (SELECT C_C_STATE FROM CAR_CUR_CARNET_TABLE with (nolock) WHERE C_C_CARNET_NUMBER=@TIRNumber) <> 0
					BEGIN
						SELECT CAR_CUR_CARNET_TABLE.C_C_CARNET_NUMBER as TIRNumber, 2 AS RESULT,  
							CAR_CUR_SYS_ASSOC_DETAILS.C_XST_ASSOCIATION_TXT as AssociationShortName, 					 
						(SELECT COUNT(*) FROM Dispatch.dbo.CAR_DISP_CARNETS with (nolock)  WHERE Dispatch.dbo.CAR_DISP_CARNETS.D_C_CARNET_NUMBER = @TIRNumber AND D_C_RECORD_STATE NOT IN (0,2,4,32,64,16384)) AS NoOfDischarges -- not rejected
						FROM CAR_CUR_CARNET_TABLE with (nolock)
						LEFT OUTER JOIN CAR_CUR_SYS_ASSOC_DETAILS with (nolock) ON CAR_CUR_CARNET_TABLE.C_C_ASSOCIATION = CAR_CUR_SYS_ASSOC_DETAILS.C_XST_ASSOCIATION  						
						WHERE (CAR_CUR_CARNET_TABLE.C_C_CARNET_NUMBER = @TIRNumber)  
					END
					ELSE
					BEGIN
						SELECT CAR_CUR_CARNET_TABLE.C_C_CARNET_NUMBER as TIRNumber, 2 AS RESULT,  
							CAR_CUR_SYS_ASSOC_DETAILS.C_XST_ASSOCIATION_TXT as AssociationShortName, 					 
						(SELECT COUNT(*) FROM Dispatch.dbo.CAR_DISP_CARNETS with (nolock)  WHERE Dispatch.dbo.CAR_DISP_CARNETS.D_C_CARNET_NUMBER = @TIRNumber AND D_C_RECORD_STATE NOT IN (0,2,4,32,64,16384)) AS NoOfDischarges -- not rejected
						FROM CAR_CUR_CARNET_TABLE with (nolock)
						LEFT OUTER JOIN CAR_CUR_PACKET_TABLE with (nolock) ON C_P_FACT_PKT_CARNET_NUMBER=(((CONVERT(INT,(@TIRNumber-1)/50))*50)+1) 
						LEFT OUTER JOIN CAR_CUR_SYS_ASSOC_DETAILS with (nolock) ON CAR_CUR_PACKET_TABLE.C_P_FACT_ASSOCIATION = CAR_CUR_SYS_ASSOC_DETAILS.C_XST_ASSOCIATION  
						WHERE (CAR_CUR_CARNET_TABLE.C_C_CARNET_NUMBER = @TIRNumber)  
					END
				END 
				ELSE -- the carnet is not in circulation
				BEGIN 
					SELECT @TIRNumber AS TIRNumber, 4 AS RESULT    
				END 
			END  
		END  
		ELSE -- the carnet is not in circulation (Status 4)
		BEGIN  
			SELECT @TIRNumber AS TIRNumber, 4 AS RESULT  
		END 
	END 
END 
