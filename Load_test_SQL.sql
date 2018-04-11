Declare  @cnt Tinyint
set @cnt = 1
while (@cnt<100)
begin
	select count(*) cnt, INVOICEACCOUNT from AP_SALES2 group by INVOICEACCOUNT order by cnt desc
end
