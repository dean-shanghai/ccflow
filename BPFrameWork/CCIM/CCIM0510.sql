--2012.5新增
CREATE TABLE [dbo].[UserRecentContact](
	[UserID] [varchar](50) NOT NULL,
	[ContactID] [varchar](50) NULL,
	[ContactType] [int] NULL,
	[ContactTime] [varchar](50) NULL,
	[OrderID] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0-User  1-Group' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRecentContact', @level2type=N'COLUMN',@level2name=N'ContactType'

GO


--只获取前20个
CREATE PROCEDURE [dbo].[UserGetRecentContactList] 
   @userId varchar(20)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 20 UserID,ContactID,ContactType,ContactTime,OrderID from UserRecentContact  where UserID=@userId ORDER BY CONVERT(datetime,ContactTime) DESC

END

GO

-- =============================================
-- Author:		Li Jian
-- Create date: 
-- Description:	用户更新最近联系人列表
-- =============================================
CREATE PROCEDURE [dbo].[UpdateRecentContact] 
	-- Add the parameters for the stored procedure here
	--@userID varchar(20),--用户ID
	@userID nvarchar(20),--用户ID
	@receiveID nvarchar(20)-- 最近联系人ID
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
   SET NOCOUNT ON;
   
   DECLARE @existUserID int;
   DECLARE @cTime varchar(50);

   SET @cTime= CONVERT(varchar(4),year(getdate()))+'-'+CONVERT(varchar(4),month(getdate()))+'-'+CONVERT(varchar(2),day(getdate()))+' '+CONVERT(varchar(2),datepart(hh,getdate()))+':'+CONVERT(varchar(2),datepart(mi,getdate()))+':'+CONVERT(varchar(2),datepart(ss,getdate()))+'.'+CONVERT(varchar(10),datepart(ms,getdate()))

   SELECT @existUserID=COUNT(*) FROM UserRecentContact WHERE UserID=@userID AND ContactID=@receiveID 
   
   IF @existUserID=0--列表中没有该用户，则添加记录
    begin 
      INSERT INTO UserRecentContact(UserID,ContactID,ContactType,ContactTime,OrderID) VALUES(@userID,@receiveID,0,@cTime,0)
    end
   else --列表中已存在，则更新时间
   begin
	  update UserRecentContact set ContactTime=@cTime where UserID=@userID AND ContactID=@receiveID
   end
END

GO
