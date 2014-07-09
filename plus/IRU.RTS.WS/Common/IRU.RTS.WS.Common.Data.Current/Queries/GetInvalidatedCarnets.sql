-- First query set
with OrderedOrders as
(
    select 
		ROW_NUMBER() OVER (ORDER BY s.s_c_creation_date asc) AS 'RowNumber'
		,s.s_c_carnet_number as [Number]
		,s.s_c_expiry_date as [ExpiryDate]
		,i.c_i_association as [IssuingAssociation]
		,a.c_xst_association_txt as [IssuingAssociationName]
		,i.c_i_holder as [Holder]
		,s.s_c_declare_date as [DateOfDeclaration]
		,s.s_c_creation_date as [DateOfInvalidation]
		,c.c_c_state as [MotiveCode]
	from
		dbo.CAR_CUR_STOPPED_CARNETS s with(nolock) 
	left join
		dbo.CAR_CUR_ISSUE_TABLE i with(nolock) on s.s_c_carnet_number = i.c_i_carnet_number 
	left join
		dbo.CAR_CUR_SYS_ASSOC_DETAILS a with(nolock) on i.c_i_association = a.c_xst_association 
	inner join
		dbo.CAR_CUR_CARNET_TABLE c with(nolock) on c.c_c_carnet_number = s.s_c_carnet_number
	where
		s.s_c_carnet_number >= @MinTIRCarnetNumber and
		s.s_c_creation_date between @DateFrom and @DateTo
) 
select 
	* 
from OrderedOrders 
where 
	RowNumber BETWEEN @Offset+1 AND @Offset+@Count

-- Second query set	
select
	count(s.s_c_carnet_number) as [CountOfFoundStoppedCarnets]
	,min(s.s_c_creation_date) as [MinOfDateOfInvalidation]
	,max(s.s_c_creation_date) as [MaxOfDateOfInvalidation]
from
	dbo.CAR_CUR_STOPPED_CARNETS s with(nolock)
where
	s.s_c_carnet_number >= @MinTIRCarnetNumber and
	s.s_c_creation_date between @DateFrom and @DateTo