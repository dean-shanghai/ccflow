--2012.5.17
ALTER TABLE Users ADD SID varchar(20) null

GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	当用户登录成功后，更新用户在线信息
-- =============================================
ALTER PROCEDURE [dbo].[UserUpdateOnlineInfo] 
	-- Add the parameters for the stored procedure here
	--@userID varchar(20),--用户ID 
	@userID nvarchar(20),--用户ID 
	--@IP varchar(15) ,--用户IP
	@IP nvarchar(15) ,--用户IP
    @Port int,--端口
    @SID varchar(20), --SID
    @NetClass tinyint--网络类型
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	--update systemUsers set lastIp=@IP,lastPort=@Port,lastNetClass=@NetClass,lastDate=GetDate(),onlineState=1 where userID=@userID
	update Users set lastIp=@IP,lastPort=@Port,lastNetClass=@NetClass,lastDate=GetDate(),[SID]=@SID,onlineState=1 where userID=@userID
END


GO







