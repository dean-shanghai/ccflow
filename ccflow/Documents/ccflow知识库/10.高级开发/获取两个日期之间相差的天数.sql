set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


-- =============================================
-- Author:		<ccflow>
-- Create date: <2003-04-02>
-- Description:	<GetSpdays tiaoxiushijiancong is from day, dao is to day>
-- =============================================
ALTER FUNCTION [dbo].[GetSpdays]
(
  @diaoxiushijiancong varchar(100), @dao varchar(200)
)
RETURNS  INT
AS
BEGIN
/* 获取两个日期天数 */
IF LEN(@diaoxiushijiancong) < 10  
BEGIN
RETURN 0;  
END   

IF LEN(@dao) < 10  
BEGIN
RETURN 0;
END

RETURN DateDiff(day,@diaoxiushijiancong,@dao) 

RETURN 0;
END;




