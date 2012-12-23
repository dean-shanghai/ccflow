--2012.5.18
-- =============================================
-- Author:		Li Jian
-- Create date: 
-- Description:	更新用户签名
-- =============================================
CREATE PROCEDURE [dbo].[UserUpdateSign] 
	@userID nvarchar(20),--用户ID 
    @Sign varchar(200) --签名
AS
BEGIN
	SET NOCOUNT ON;

	update Users set Remark=@Sign where userID=@userID
END

GO


--2012.5.19
-- =============================================
-- Author:		Li Jian
-- Create date: 
-- Description:	获得某用户的签名
-- =============================================
CREATE PROCEDURE [dbo].[UserGetUsersSignsByUserID] 
	@userID nvarchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Remark from Users where userID=@userID

END

GO



-- =============================================
-- Author:		Li Jian
-- Create date: 
-- Description:	获得所有用户的签名
-- =============================================
CREATE PROCEDURE [dbo].[GetAllUsersSigns] 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT UserID,Remark from Users where UserID<>'admin'

END


GO
