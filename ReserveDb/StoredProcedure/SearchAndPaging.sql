
CREATE PROCEDURE [dbo].[SearchAndPaging]
	@Title nvarchar(50) , @LocationType smallint , @pageNumber int , @itemCount int
AS
BEGIN
 declare @q nvarchar(500) 
 
set @q = 'select * from Locations where 1=1 '
	+ IIF(@Title is not null , 'and Title = ' + @Title , '')
	+ IIF(@LocationType is not null , 'and LocationType = ' + CAST(@LocationType as varchar(20)),'')
	+ 'Offset ' + CAST((@pageNumber -1)*@itemCount as varchar(50))
	+ 'fetch next ' + @itemCount + 'rows only'

	exec (@q)

END