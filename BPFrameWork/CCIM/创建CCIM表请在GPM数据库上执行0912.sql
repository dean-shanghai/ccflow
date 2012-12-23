USE GPM

/* 初试化系统参数 */

/*
DELETE Sys_GloVar WHERE GroupKey='CCIM';
INSERT INTO SYS_GloVar(No,Name,Val,GroupKey,Note) VALUES 
('NetHardDiskURI','网络硬盘:地址','http://127.0.0.1/HardDisk/','CCIM','');
INSERT INTO SYS_GloVar(No,Name,Val,GroupKey,Note) VALUES 
('NetHardDiskUserName','网络硬盘:访问用户名','administrator','CCIM','');
INSERT INTO SYS_GloVar(No,Name,Val,GroupKey,Note) VALUES 
('NetHardDiskURI','网络硬盘:密码','http://127.0.0.1/HardDisk/','CCIM','');
*/ 
GO
CREATE TABLE [dbo].[plDepartment](
	[DepartmentID] [int]  NOT NULL,
	[Name] [nvarchar](50) NULL,
	[EngName] [nvarchar](50) NULL,
	[Code] [nvarchar](50) NULL,
	[ParentID] [int] NULL,
	[Comment] [nvarchar](255) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
	[Phone] [char](30) NULL,
	[Fax] [char](30) NULL,
	[orderID] [int] NULL,
	[isAbandan] [bit] NULL,
 CONSTRAINT [PK_PLDEPARTMENT] PRIMARY KEY CLUSTERED 
(
	[DepartmentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
 

CREATE TABLE [dbo].[Staff](
	[StaffID] [int]  NOT NULL,
	[StaffName] [nvarchar](50)   NULL,
	[Sex] [nvarchar](10) NULL,
	[DepartmentID] [int] NOT NULL,
	[Tel] [nvarchar](50) NULL,
	[Mobile] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Address] [nvarchar](100) NULL,
	[PostCode] [nvarchar](50) NULL,
	[Fired] [nvarchar](10) NULL,
	[HireProperty] [nvarchar](10) NULL,
	[SID] [nvarchar](20) NULL,
	[Memo] [nvarchar](200) NULL,
 CONSTRAINT [XPK职员表] PRIMARY KEY CLUSTERED 
(
	[StaffID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO





CREATE TABLE [dbo].[SMS](
	[msgID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[smsMsgContent] [text] NOT NULL,
	[sendID] [nchar](20) NOT NULL,
	[datetime] [datetime] NOT NULL CONSTRAINT [DF_SMS_sendfailedCount]  DEFAULT (getdate()),
 CONSTRAINT [PK_SMS] PRIMARY KEY CLUSTERED 
(
	[msgID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'短消息内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMS', @level2type=N'COLUMN',@level2name=N'smsMsgContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息发送者ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMS', @level2type=N'COLUMN',@level2name=N'sendID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息发送失败次数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMS', @level2type=N'COLUMN',@level2name=N'datetime'
GO


CREATE TABLE [dbo].[smsUser](
	[ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[msgID] [numeric](18, 0) NOT NULL,
	[receiveID] [nchar](20) NOT NULL,
	[sendFailedCount] [tinyint] NULL CONSTRAINT [DF_smsUser_sendFailedCount]  DEFAULT ((0)),
	[isSendSuccess] [bit] NULL CONSTRAINT [DF_smsUser_isSendSuccess]  DEFAULT ((0)),
	[lastDate] [datetime] NULL,
 CONSTRAINT [PK_smsUser] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标识短消息发送是否成功' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'smsUser', @level2type=N'COLUMN',@level2name=N'isSendSuccess'
GO
ALTER TABLE [dbo].[smsUser]  WITH CHECK ADD  CONSTRAINT [FK_smsUser_SMS] FOREIGN KEY([msgID])
REFERENCES [dbo].[SMS] ([msgID])
GO
ALTER TABLE [dbo].[smsUser] CHECK CONSTRAINT [FK_smsUser_SMS]
GO



CREATE TABLE [dbo].[sendCount](
	[MsgID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[smsMsgContent] [text] NOT NULL,
	[saveDate] [datetime] NOT NULL CONSTRAINT [DF_sendCount_saveDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_sendCount] PRIMARY KEY CLUSTERED 
(
	[MsgID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'短消息内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sendCount', @level2type=N'COLUMN',@level2name=N'smsMsgContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'写入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sendCount', @level2type=N'COLUMN',@level2name=N'saveDate'
GO


CREATE TABLE [dbo].[RecordMsgUser](
	[ID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[MsgId] [numeric](18, 0) NOT NULL,
	[ReceiveID] [nchar](20) NOT NULL,
	[isSendSuccess] [bit] NOT NULL CONSTRAINT [DF_RecordMsgUser_isSendSuccess]  DEFAULT ((0)),
	[isSave] [bit] NOT NULL CONSTRAINT [DF_RecordMsgUser_isSave]  DEFAULT ((0)),
 CONSTRAINT [PK_GroupRecordMsg] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordMsgUser', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordMsgUser', @level2type=N'COLUMN',@level2name=N'MsgId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息接收者ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordMsgUser', @level2type=N'COLUMN',@level2name=N'ReceiveID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标识离线消息是否发送成功，0为否，1为是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordMsgUser', @level2type=N'COLUMN',@level2name=N'isSendSuccess'
GO




CREATE TABLE [dbo].[RecordMsg](
	[MsgID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[sendID] [nchar](90) NULL,
	[msgDateTime] [datetime] NULL CONSTRAINT [DF_RecordMsg_msgDateTime]  DEFAULT (getdate()),
	[msgContent] [text] NULL,
	[ImageInfo] [ntext] NULL,
	[fontName] [nchar](90) NULL,
	[fontSize] [float] NULL,
	[fontBold] [bit] NULL CONSTRAINT [DF_RecordMsg_fontBold]  DEFAULT ((0)),
	[fontItalic] [bit] NULL CONSTRAINT [DF_RecordMsg_fontItalic]  DEFAULT ((0)),
	[fontStrikeout] [bit] NULL CONSTRAINT [DF_RecordMsg_fontStrikeout]  DEFAULT ((0)),
	[fontUnderline] [bit] NULL CONSTRAINT [DF_RecordMsg_fontUnderline]  DEFAULT ((0)),
	[fontColor] [int] NULL,
	[InfoClass] [int] NULL,
	[SMSInfo] [tinyint] NULL CONSTRAINT [DF_RecordMsg_isSendSMS]  DEFAULT ((0)),
	[GroupID] [int] NULL CONSTRAINT [DF_RecordMsg_GroupID]  DEFAULT ((-1)),
	[SendUserID] [nchar](990) NULL,
 CONSTRAINT [PK_RecordMsg] PRIMARY KEY CLUSTERED 
(
	[MsgID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'短信发送类型：0代表不发送短信，1代表在线不发送，2代表在不在线都发送' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordMsg', @level2type=N'COLUMN',@level2name=N'SMSInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'如果是群组消息，则不为－1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RecordMsg', @level2type=N'COLUMN',@level2name=N'GroupID'
GO

CREATE TABLE [dbo].[CustomGroup](
	[GroupID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](20) NULL,
	[Notice] [nvarchar](100) NULL,
	[users] [ntext] NULL,
	[UserID] [nvarchar](20) NULL,
	[CreateDateTime] [datetime] NULL CONSTRAINT [DF_CustomGroup_CreateDateTime]  DEFAULT (getdate()),
 CONSTRAINT [PK_CustomGroup] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO







CREATE TABLE [dbo].[Users](
	[UserID] [nvarchar](20) NOT NULL,
	[CustomUserID] [int] NULL,
	[StaffID] [int] NULL,
	[Password] [nvarchar](32) NULL,
	[OnlineStatus] [int] NULL,
	[IsStaff] [int] NULL,
	[UsableStatus] [int] NULL,
	[VisitCount] [int] NULL,
	[IsPublic] [int] NULL,
	[LastLoginTime] [datetime] NULL,
	[LastRemindTime] [datetime] NULL,
	[IsReminding] [int] NULL,
	[LastLoginAddress] [nvarchar](50) NULL,
	[RegDate] [datetime] NULL,
	[orderID] [int] NULL,
	[nickName] [nvarchar](20) NULL,
	[headPicIdx] [int] NULL,
	[onlineState] [tinyint] NULL,
	[lastIp] [char](30) NULL,
	[lastPort] [int] NULL,
	[lastNetClass] [tinyint] NULL,
	[lastDate] [datetime] NULL,
	[onlineDateLength] [int] NULL,
	[CreateGroupMax] [tinyint] NULL,
	[isSendSMS] [tinyint] NULL,
	[isEditUserData] [tinyint] NULL,
	[isAdmin] [bit] NULL,
	[Remark] [nvarchar](200) NULL,
	[isAbandan] [bit] NULL,
	[DepsVersion] [char](32) NULL,
	[UsersVersion] [char](32) NULL,
	[UsersCount] [int] NULL,
	[DepsCount] [int] NULL,
	[headPicFile] [nvarchar](256) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO











-- =============================================
-- Author:		Li Jian
-- Create date:  2011-5-1
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[CustomGroupCreateOrUpdate] 
    @GroupID int,
    @GroupName nvarchar(20), 
    @Notice nchar(100),
    @Users text,
	@userID nvarchar(20) 
AS
BEGIN

	declare @createMax int 
	declare @createCount int 
    declare @returnGroupID int
        

    set @createMax=1 
    set @createCount=0 
    set @returnGroupID=0
    
	SET NOCOUNT ON;

   if @GroupID=0 
   begin
    select @createCount=count(*) from CustomGroup where UserID=@userID
    select @createMax=CreateGroupMax from Users where UserID=@userID
    if @createCount<@createMax 
      begin
	    insert into CustomGroup(GroupName,Notice,users,UserID) values( @GroupName,@Notice,@Users,@userID)
        set @returnGroupID=@@IDENTITY    
      end
   end

   if @GroupID<>0 
    begin
      select @createCount=count(*) from CustomGroup where GroupID=@GroupID and UserID=@userID 
      if @createCount=0 
        begin
           set @returnGroupID=0
        end
      if @createCount=1 
        begin
	       update CustomGroup set GroupName=@GroupName,Notice=@Notice,Users=@Users where GroupID=@GroupID
           set @returnGroupID=@GroupID 
        end
    end

    return    @returnGroupID    
     
    SET   NOCOUNT   OFF   
END

GO




-- =============================================
-- Author:		Li Jian
-- Create date:  2011-5-1
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[CustomGroupGetIDs] 
	@userID nvarchar(20)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT GroupID,GroupName from customGroup where userID=@userID or users like '%' + @userID + ';%'
END


GO




-- =============================================
-- Author:		Li Jian
-- Create date:    2011-5-1
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[CustomGroupGetInfoFromGroupID] 
	@GroupID int  
AS
BEGIN
	 
	SET NOCOUNT ON;
    SELECT * from customGroup where GroupID=@GroupID
END

GO



-- =============================================
-- Author:	Li Jian	
-- Create date: 2011-5-1
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[DepAddData] 
	@DepId varchar(20), 
	@DepName nchar(20),
	@Phone char(30) ,
	@Fax char(30) ,
	@SuperiorId varchar(20) ,
	@orderID int 
AS
BEGIN
	declare @ct int
	SET NOCOUNT ON;

	select @ct=count(*) from plDepartment where DepartmentID=@DepId;


	if @ct=0
    begin
         insert into plDepartment (DepartmentID,Name,orderID,Phone,Fax,ParentId) 
		   values(@DepId,@DepName,@orderID,@Phone,@Fax,@SuperiorId)
		  return 1
    end
    else 
    begin
         return 0
    end
END


GO



-- =============================================
-- Author:	Li Jian	
-- Create date: 2011-5-1
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[DepDelData] 
	@DepId nvarchar(20) 

AS
BEGIN
	SET NOCOUNT ON;

	delete from plDepartment where DepartmentID=@DepId

END


GO



-- =============================================
-- Author:		Li Jian
-- Create date: 2011-5-1
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[DepGetData] 
	@DepId nvarchar(20),
	@flag  bit
AS
BEGIN
	SET NOCOUNT ON;

	if @flag = 0
	begin

		SELECT * from plDepartment where DepartmentID=@DepId
	end
	else
	begin

		SELECT * from plDepartment where ParentID=@DepId
	end

END


GO


-- =============================================
-- Author:	Li Jian
-- Create date:   2011-5-1
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[DepGetDepsPageBaseInfo] 
	@UserID nvarchar(20),
	@PageSize int,
	@PageIndex int
AS
BEGIN

	SET NOCOUNT ON;

   
	CREATE TABLE #PageIndex 
	(
		IndexId int IDENTITY (1, 1) NOT NULL,
		DepID int 
	)


    INSERT INTO #PageIndex(DepID) select DepartmentID from plDepartment where DepartmentID<>1 order by OrderID,DepartmentID asc 
    

    select DepartmentID as DepID,Name as DepName,ParentID as SuperiorId,OrderID from plDepartment WHERE DepartmentID in(select DepID from #PageIndex where IndexId BETWEEN @PageSize*(@PageIndex-1)+1 AND @PageSize*(@PageIndex))


END


GO

 



-- =============================================
-- Author:	Li Jian	 
-- Create date:  2011-5-1
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GroupMsgRecordInsertUsers] 
	@MsgId int,
	@ReceiveID varchar(8000) 

AS
BEGIN

	SET NOCOUNT ON;
	declare @next int  
	set @next=1
	while @next<dbo.Get_StrArrayLength(@ReceiveID,';')
		begin
			insert into RecordMsgUser (MsgId,ReceiveID)	values(@MsgId ,dbo.Get_StrArrayStrOfIndex(@ReceiveID,';',@next) )
			set @next=@next+1
		end

END



GO




-- =============================================
-- Author:	Li Jian
-- Create date:  2011-5-1
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[RecordMsgDelFromID] 
    @ID numeric (18,0),
	@userID nvarchar(20)
AS
BEGIN

	SET NOCOUNT ON;

    delete  from RecordMsgUser where ReceiveID=@userID and MsgId=@ID 

    DECLARE @Total INT 
    select  @Total=count(*) from RecordMsgUser where MsgId=@ID
    if (@Total=0)
       begin
	       delete from RecordMsg where MsgId=@ID 
       end
END

GO



-- =============================================
-- Author:	Li Jian
-- Create date: 2011-5-1
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[RecordMsgGet] 
	@userID nvarchar(20)
AS
BEGIN

	SET NOCOUNT ON;

	SELECT  * from RecordMsg,RecordMsgUser where RecordMsgUser.isSendSuccess=0 and  RecordMsgUser.ReceiveID=@userID and RecordMsg.MsgId=RecordMsgUser.MsgId
END

GO


-- =============================================
-- Author:	Li Jian		
-- Create date: 2011-5-1
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[RecordMsgGetFromId] 
	@ID numeric (18,0)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * from RecordMsg where MsgId= @ID
END


GO



-- =============================================
-- Author:	Li Jian	 
-- Create date: 2011-5-1
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[RecordMsgInsert] 
	@userID nvarchar(20),
    @ReceiveID varchar(20),
	@msgContent ntext,
	@ImageInfo ntext,
	@fontName nchar(30),
	@fontSize float,
	@fontBold bit,
	@fontItalic bit,
	@fontStrikeout bit,
	@fontUnderline bit,
	@fontColor int,
	@InfoClass int,
    @SMSInfo tinyint,
    @GroupID int 
AS
BEGIN
	SET NOCOUNT ON;

	insert into RecordMsg (sendID,
							msgContent,
							ImageInfo,
							fontName,
							fontSize,
							fontBold,
							fontItalic,
							fontStrikeout,
							fontUnderline,
							fontColor,
							InfoClass,
                            SMSInfo,
                            GroupID 
)
	values(@userID,
			@msgContent,
			@ImageInfo,
			@fontName,
			@fontSize,
			@fontBold,
			@fontItalic,
			@fontStrikeout,
			@fontUnderline ,
			@fontColor,
			@InfoClass,
            @SMSInfo,
            @GroupID
          )

     if (@ReceiveID<>'')
     begin
       insert into RecordMsgUser (MsgId,ReceiveID ) values(@@IDENTITY ,@ReceiveID )
     end
    SET   NOCOUNT   OFF   

    return  @@IDENTITY     
END



GO



-- =============================================
-- Author:	Li Jian
-- Create date: 2011-5-1
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[Search]
	@TableName     nvarchar(400),        
	@FieldName     nvarchar(2000) = '*',    
	@Condition	nvarchar(2000),				
	@Sort	nvarchar(400),				
	@StartRowIndex int = 0,
	@MaximumRows int =10,
	@TotalCountLimit int = 0,	
	@TotalCount int output
AS
BEGIN
Declare @sqlFrom nvarchar(4000)        
Declare @sqlCount nvarchar(2000)        
Declare @sqlSelect nvarchar(2000)        
Declare @sqlRowNumber nvarchar(2000)		
Declare @EndRowIndex int





	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Set @EndRowIndex = (@StartRowIndex  / @MaximumRows +1)* @MaximumRows

	set @sqlRowNumber = 'SELECT ' + @FieldName + ' ,ROW_NUMBER() OVER(ORDER BY ' + @Sort + ') AS rn FROM '
			+ @TableName + ' WHERE ' + @Condition

	Set @sqlSelect = 'SELECT  m.*  FROM (' + @sqlRowNumber + ') AS m'
		+ ' WHERE m.rn>' + STR(@StartRowIndex) + ' and m.rn <=' + STR(@EndRowIndex) 
		+ ' ORDER BY ' + @Sort
	exec sp_executesql @sqlSelect

	Set @sqlCount = 'SELECT @TotalCount=COUNT(*)  FROM (' + @sqlRowNumber + ') AS m'
	
	if @TotalCountLimit <> 0 
	begin
		set @sqlCount = @sqlCount + ' WHERE  m.rn<=' + str(@TotalCountLimit)
	end

	exec sp_executesql @sqlCount,N'@TotalCount int out ',@TotalCount out

END

GO



-- =============================================
-- Author:	Li Jian	
-- Create date: 2011-5-1
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[Search1]
	-- Add the parameters for the stored procedure here
	@TableName     nvarchar(400),        
	@FieldName     nvarchar(2000) = '*',    
	@Condition	nvarchar(2000),				
	@Sort	nvarchar(400),				
	@Sort1	nvarchar(400),				
	@StartRowIndex int = 0,
	@MaximumRows int =10,
	@TotalCountLimit int = 0,	
	@TotalCount int output
AS
BEGIN
Declare @sqlFrom nvarchar(4000)        
Declare @sqlCount nvarchar(2000)        
Declare @sqlSelect nvarchar(2000)        
Declare @sqlRowNumber nvarchar(2000)		
Declare @EndRowIndex int





	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Set @EndRowIndex = (@StartRowIndex  / @MaximumRows +1)* @MaximumRows

	set @sqlRowNumber = 'SELECT ' + @FieldName + ' ,ROW_NUMBER() OVER(ORDER BY ' + @Sort + ') AS rn FROM '
			+ @TableName + ' WHERE ' + @Condition

	Set @sqlSelect = 'SELECT  m.*  FROM (' + @sqlRowNumber + ') AS m'
		+ ' WHERE m.rn>' + STR(@StartRowIndex) + ' and m.rn <=' + STR(@EndRowIndex) 
		+ ' ORDER BY ' + @Sort1
	exec sp_executesql @sqlSelect

	Set @sqlCount = 'SELECT @TotalCount=COUNT(*)  FROM (' + @sqlRowNumber + ') AS m'
	
	if @TotalCountLimit <> 0 
	begin
		set @sqlCount = @sqlCount + ' WHERE  m.rn<=' + str(@TotalCountLimit)
	end

	exec sp_executesql @sqlCount,N'@TotalCount int out ',@TotalCount out

END


GO


-- =============================================
-- Author:	Li Jian
-- Create date: 2011-5-1
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[smsDelNoSendFailed] 
	 
AS
BEGIN
	 
	SET NOCOUNT ON;
    delete  from  smsUser where smsUser.isSendSuccess=0
END

GO



-- =============================================
-- Author:	Li Jian
-- Create date: 2011-5-1
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[smsGetNoSend] 
	
	
AS
BEGIN

	SET NOCOUNT ON;

	SELECT smsUser.ID,sms.sendID as sendID,sms.msgID as msgID,
           --(select systemusers.userName from systemusers,sms where systemusers.userID=sms.sendID )as UserName,
            sms.smsMsgContent as smsMsgContent,systemUsers.GSM as  GSM,systemUsers.phs as phs 
    from systemusers,smsUser,sms  --
    where smsUser.receiveID=systemusers.userID and smsUser.msgID=sms.msgID and smsUser.isSendSuccess=0 and (systemusers.phs is not null or systemusers.GSM is not null)
END

GO



-- =============================================
-- Author:	Li Jian
-- Create date: 2011-5-1
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[smsGetSendSuccessfulCount] 
 
AS
BEGIN
 	SET NOCOUNT ON;


	SELECT count(*) from smsUser where isSendSuccess=1
END



GO



-- =============================================
-- Author:	Li Jian
-- Create date: 2011-5-1
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[smsInsert] 
	@ReceiveID varchar(8000) ,
    @sendID varchar(20) ,
	@msgContent varchar(1000) 
AS
BEGIN
    declare @MsgId numeric(18,0)
	SET NOCOUNT ON;

	insert SMS(sendID,smsMsgContent) values(@sendID,@msgContent)

    set @MsgId=  @@IDENTITY

    declare @next int  

	set @next=1
	while @next<dbo.Get_StrArrayLength(@ReceiveID,';') 
		begin
			insert into smsUser (MsgId,ReceiveID) values(@MsgId ,dbo.Get_StrArrayStrOfIndex(@ReceiveID,';',@next))
			set @next=@next+1
		end
    SET   NOCOUNT   OFF   
END


GO


-- =============================================
-- Author:	Li Jian
-- Create date: 2011-5-1
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[smsSendFailed] 

	@ID numeric(18,0)  
AS
BEGIN

	SET NOCOUNT ON;

	update smsUser set sendFailedCount=sendFailedCount+1,lastDate=getdate() where ID=@ID
    SET NOCOUNT off;
END

GO


-- =============================================
-- Author:		Li Jian
-- Create date: 2011-5-1
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[smsSendSuccess] 

	@ID numeric(18,0) 
AS
BEGIN

	SET NOCOUNT ON;

	update smsUser set isSendSuccess=1,lastDate=getdate() where id=@ID
    SET NOCOUNT off;
END

GO


-- =============================================
-- Author:	Li Jian
-- Create date: 2011-5-1
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SYSTEMRecordMsgGet] 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  * from RecordMsg,RecordMsgUser where RecordMsgUser.isSendSuccess=0 and  RecordMsg.SendID='SYSTEM' and RecordMsg.MsgId=RecordMsgUser.MsgId
END

GO



CREATE PROCEDURE [dbo].[UpdatePass]

AS
BEGIN

Declare @sqlSelect nvarchar(1000)      

	SET NOCOUNT ON;
	Set @sqlSelect = 'update [09company] set ispass = 0 where id in (select id from (select id,ispass,dateadd(month,3,(select top 1 createdate from [09CompanyBeian] where [09CompanyBeian].companyid = [09Company].id  order By Createdate desc)) as createdate  from [09Company]) as t where t.createdate is not null and t.createdate<getdate())'
		
	exec sp_executesql @sqlSelect

END


GO


CREATE PROCEDURE [dbo].[UserAddData] 
	@userID varchar(20), 
	@userName nchar(20),
	@DepId varchar(20) ,
	@Phone char(30) ,
	@Password char(32),
	@GSM char(11),
	@phs char(30) ,
	@Email char(30),
	@address nchar(50),
	@post nchar(10),
	@CreateGroupMax int,
	@isSendSMS bit,
	@isEditUserData bit,
	@orderID int,
	@isAbandan bit,
	@isAdmin bit
AS
BEGIN
	declare @ct int

	SET NOCOUNT ON;

	select @ct=count(*) from SystemUsers where userID=@userID;

	if @ct=0
    begin
	insert into SystemUsers (userID ,userName,DepId,Phone,Password,GSM,phs,Email,address,post,CreateGroupMax,isSendSMS,isEditUserData,orderID,isAbandan,isAdmin) 
		   values(@userID ,@userName,@DepId,@Phone,@Password,@GSM,@phs,@Email,@address,@post,@CreateGroupMax,@isSendSMS,@isEditUserData,@orderID,@isAbandan,@isAdmin)
		  return 1
    end
    else 
    begin
         return 0
    end
	
END

GO


CREATE PROCEDURE [dbo].[UserChangePassword] 
	@userID nvarchar(20),
	@PasswordOld varchar(32),
	@PasswordNew varchar(32) 
AS
BEGIN
	declare @userCount int
	SET NOCOUNT ON;

    select  @userCount=count(*) from Users where UserID=@userID and password=@PasswordOld
    
    if @userCount=0 
       return 0
	
    --update systemUsers  set Password=@PasswordNew where UserID=@userID 
    update Users  set Password=@PasswordNew where UserID=@userID 
    
    return 1 
END


GO



CREATE PROCEDURE [dbo].[UserDelData] 
	@userID nvarchar(20) 
AS
BEGIN

	SET NOCOUNT ON;

	delete from Users where userID=@userID
END

GO


CREATE PROCEDURE [dbo].[userGetBaseData] 
	@userId nvarchar(20)

AS
BEGIN
	SET NOCOUNT ON;

	SELECT a.UserID,b.StaffName as userName,b.PostCode as post,
      b.Tel as phone,b.Mobile as GSM,b.Fax as phs 
    FROM Users a,Staff b WHERE a.StaffID=b.StaffID AND a.userID=@userId

END

GO

create PROCEDURE [dbo].[UserGetByDep] 
   @depId varchar(20)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  RTRIM(LTRIM(userID)) AS userID,RTRIM(LTRIM(userName)) AS userName from SystemUsers  where DepId=@depId order by orderID
END

GO



CREATE PROCEDURE [dbo].[UserGetList] 
   @userId varchar(20)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  userID,userName,DepId,orderID from SystemUsers  where DepId<>1
END

GO


CREATE PROCEDURE [dbo].[UserGetPageUsersBaseInfo] 
	@UserID nvarchar(20),
	@PageSize int,
	@PageIndex int
AS
BEGIN
	SET NOCOUNT ON;
 
	CREATE TABLE #PageIndex 
	(
		IndexId int IDENTITY (1, 1) NOT NULL,
		--UserID varchar(20)
		UserID nvarchar(20)
	)

    INSERT INTO #PageIndex(UserID) select a.userID from Users a,Staff b 
      where a.StaffID=b.StaffID AND b.DepartmentID<>1 AND b.Fired='在职' order by a.orderID,a.userID asc 
    
    select a.userID,b.Staffname as userName,b.DepartmentID as DepId,a.orderID 
      from Users a,Staff b WHERE a.StaffID=b.StaffID AND a.userID in(select userID from #PageIndex 
      where IndexId BETWEEN @PageSize*(@PageIndex-1)+1 AND @PageSize*(@PageIndex))

END

GO


CREATE PROCEDURE [dbo].[UserGetUserDataFromUserID] 
	@userID nvarchar(20)

	 
AS
BEGIN

	SET NOCOUNT ON;

	SELECT * from Users where userID=@userID

END

GO


CREATE PROCEDURE [dbo].[UserIniPassword] 
	@userID nvarchar(20)
AS
BEGIN
  update Users  set Password='E10ADC3949BA59ABBE56E057F20F883E'  where UserID=@userID 
END

GO


CREATE PROCEDURE [dbo].[userIsExist] 
	@userID nvarchar(20)
AS
BEGIN
	SET NOCOUNT ON;
    declare @userCount tinyint
    set @userCount=0
	SELECT @userCount=count(userID) from Users where UserID=@userID
    return @userCount
END

GO



CREATE PROCEDURE [dbo].[userIsPassword] 
	@userID nvarchar(20), 
	@password nvarchar(32)
AS
BEGIN
 	SET NOCOUNT ON;
    declare @count int
	SELECT @count=count(*) from Users where userID=@userID and password=@password
    return  @count
END

GO


CREATE PROCEDURE [dbo].[UserUpdateData] 
		@userID varchar(20), 
	@userName nchar(20),
	@DepId varchar(20) ,
	@Phone char(30) ,
	@Password char(32),
	@GSM char(11),
	@phs char(30) ,
	@Email char(30),
	@address nchar(50),
	@post nchar(10),
	@CreateGroupMax int,
	@isSendSMS bit,
	@isEditUserData bit,
	@orderID int,
	@isAbandan bit ,
	@isAdmin bit
AS
BEGIN

	SET NOCOUNT ON;

	if @Password != ''
	begin
		update SystemUsers set Password=@Password where userID=@userID
	end
	update SystemUsers set  userName=@userName,
							DepId=@DepId,
							Phone=@Phone,
							GSM=@GSM,
							phs=@phs,
							Email=@Email,
							address=@address,
							post=@post,
							CreateGroupMax=@CreateGroupMax,
							isSendSMS=@isSendSMS,
							isEditUserData=@isEditUserData,
							orderID=@orderID,
							isAbandan=@isAbandan,
							isAdmin=@isAdmin
	where userID=@userID
END

GO



CREATE PROCEDURE [dbo].[UserUpdateOnlineInfo] 
	@userID nvarchar(20),--用户ID 
	@IP nvarchar(15) ,--用户IP
    @Port int,--端口
    @NetClass tinyint--网络类型
AS
BEGIN

	SET NOCOUNT ON;

	update Users set lastIp=@IP,lastPort=@Port,lastNetClass=@NetClass,lastDate=GetDate(),onlineState=1 where userID=@userID
END

GO


CREATE PROCEDURE [dbo].[UserUpdateOnlineState] 
	@userID nvarchar(20),
	@State tinyint
AS
BEGIN

	SET NOCOUNT ON;
   
   IF @State=0
    begin 
      update Users set onlineState=@State,onlineDateLength=(select isnull(onlineDateLength,0)+datediff(ss,lastDate,getdate()) from Users where UserID=@userID) where UserID=@userID 
    end
   else
   begin
	  update Users set onlineState=@State where UserID=@userID
   end
END


GO



CREATE function [dbo].[Get_StrArrayLength]
(
  @str varchar(8000),  
  @split varchar(10)  
)
returns int
as
begin
 declare @location int
 declare @start int
 declare @length int

 set @str=ltrim(rtrim(@str))
 set @location=charindex(@split,@str)
 set @length=1
 while @location<>0
 begin
   set @start=@location+1
   set @location=charindex(@split,@str,@start)
   set @length=@length+1
 end
 return @length
end

GO


CREATE function [dbo].[Get_StrArrayStrOfIndex]
(
 @str varchar(8000),  
 @split varchar(10),  
 @index int 
)
returns varchar(1024)
as
begin
 declare @location int
 declare @start int
 declare @next int
 declare @seed int

 set @str=ltrim(rtrim(@str))
 set @start=1
 set @next=1
 set @seed=len(@split)
 
 set @location=charindex(@split,@str)
 while @location<>0 and @index>@next
 begin
   set @start=@location+@seed
   set @location=charindex(@split,@str,@start)
   set @next=@next+1
 end
 if @location =0 select @location =len(@str)+1 
 
 return substring(@str,@start,@location-@start)
end

GO



CREATE FUNCTION [dbo].[MD5] 
(
    @src nvarchar(max),
    @type int = 32

)
RETURNS nvarchar(max)
WITH EXECUTE AS CALLER
AS
BEGIN
    DECLARE @smd5 varchar(34)

    SELECT @smd5 =sys.fn_VarBinToHexStr(hashbytes('MD5', @src));

    IF @type=16
        SELECT @smd5 = SUBSTRING(@smd5,11,16)   
    ELSE
        SELECT @smd5 = SUBSTRING(@smd5,3,32)    

    RETURN @smd5
END


GO

-------------------  桥接语句

--桥接ccflow和IM的部门表
CREATE PROCEDURE sp_UpdateDept
  @DeptID int
AS
BEGIN
DECLARE @Exists int
DECLARE @DeptName varchar(500)
DECLARE @ParentID int
DECLARE @UsersCount int
DECLARE @DeptCount int
DECLARE @DeptVer varchar(500)

SELECT @Exists=COUNT(*) FROM plDepartment WHERE DepartmentID=@DeptID
IF (@Exists=0)
  BEGIN
    SELECT @DeptName=[Name],@ParentID=ParentID FROM Port_Dept WHERE DepartmentID=@DeptID
    INSERT INTO plDepartment(DepartmentID,[Name],ParentID,orderID) VALUES(@DeptID,@DeptName,@ParentID,0)

    SELECT @UsersCount=COUNT(*) FROM Users
    IF (@UsersCount<>0)
      BEGIN
        SELECT DISTINCT @DeptVer=DepsVersion FROM Users
        SELECT @DeptCount=DepsCount FROM Users
        SET @DeptVer=LEFT(@DeptVer,29)+RIGHT(CAST((CAST(RIGHT(@DeptVer,3) as int)+1001) as varchar),3)
        SET @DeptCount=@DeptCount+1
        UPDATE Users SET DepsVersion=@DeptVer,DepsCount=@DeptCount
      END
  END

END

GO



--桥接ccflow和IM的人员表
CREATE PROCEDURE sp_UpdateUser
  @UserID int
AS
BEGIN
DECLARE @Exists int
DECLARE @UserNo varchar(50)
DECLARE @UserName varchar(50)
DECLARE @DeptID int
DECLARE @UsersCount int
DECLARE @UsersVer varchar(32)
DECLARE @DeptCount int
DECLARE @DeptVer varchar(32)
DECLARE @PW varchar(32)

SELECT @Exists=COUNT(*) FROM Staff WHERE StaffID=@UserID
IF (@Exists=0)
  BEGIN
SET @PW='307C1506167B6414'

    SELECT @UserNo=[No],@UserName=[Name],@DeptID=DepartmentID FROM Port_Emp WHERE StaffID=@UserID
    INSERT INTO Staff(StaffID,StaffName,DepartmentID,Sex,Fired) VALUES(@UserID,@UserName,@DeptID,'男','在职')

    SELECT @UsersCount=COUNT(*) FROM Users
    IF (@UsersCount<>0)
      BEGIN
        SELECT DISTINCT @DeptVer=DepsVersion FROM Users
        SELECT @DeptCount=DepsCount FROM Users
        SELECT DISTINCT @UsersVer=UsersVersion FROM Users
        SELECT @UsersCount=UsersCount FROM Users

        SET @UsersVer=LEFT(@UsersVer,26)+RIGHT(CAST((CAST(RIGHT(@UsersVer,6) as int)+1000001) as varchar),6)
        SET @UsersCount=@UsersCount+1
        UPDATE Users SET UsersVersion=@UsersVer,UsersCount=@UsersCount

        INSERT INTO Users(UserID,StaffID,[Password],IsStaff,UsableStatus,VisitCount,IsPublic,RegDate,orderID,
          headPicIdx,CreateGroupMax,isSendSMS,isEditUserData,isAdmin,DepsVersion,UsersVersion,
          UsersCount,DepsCount) VALUES(@UserNo,@UserID,@PW,1,0,0,0,'2012-04-12',1,1,250,0,0,0,@DeptVer,@UsersVer,@UsersCount,@DeptCount)

      END
    ELSE
      BEGIN
        --第一个用户
        SET @DeptVer='6dfd0f116118516ef750c47812afc000'
        SET @UsersVer='1c46c15d875e72059a4273edca000000'

        SELECT @DeptCount=COUNT(*) FROM plDepartment

        INSERT INTO Users(UserID,StaffID,[Password],IsStaff,UsableStatus,VisitCount,IsPublic,RegDate,orderID,
          headPicIdx,CreateGroupMax,isSendSMS,isEditUserData,isAdmin,DepsVersion,UsersVersion,
          UsersCount,DepsCount) VALUES(@UserNo,@UserID,@PW,1,0,0,0,'2012-04-12',1,1,250,0,0,0,@DeptVer,@UsersVer,1,@DeptCount)
        
      END
  END

END

GO


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

-- add by peng
UPDATE users SET DepsCount=(select count(*) from plDepartment),
UsersCount=( select count(a.UserID) from Users a,Staff b where a.StaffID=b.StaffID and b.FIred='在职' ),
DepsVersion='0',UsersVersion='0'
go

