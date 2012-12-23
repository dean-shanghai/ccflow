DELETE FROM Port_Dept  ;
DELETE FROM Port_Station  ;
DELETE FROM Port_Emp  ;
DELETE FROM Port_EmpStation  ;
DELETE FROM Port_EmpDept  ;
 
-- Port_Dept ;
INSERT INTO Port_Dept (No,Name) VALUES('01','总经理室')  ;
INSERT INTO Port_Dept (No,Name) VALUES('0101','市场部')  ;
INSERT INTO Port_Dept (No,Name) VALUES('0102','研发部')  ;
INSERT INTO Port_Dept (No,Name) VALUES('0103','服务部')  ;
INSERT INTO Port_Dept (No,Name) VALUES('0104','财务部')  ;
INSERT INTO Port_Dept (No,Name) VALUES('0105','人力资源部')  ;

-- Port_Station ;
INSERT INTO Port_Station (No,Name,StaGrade) VALUES('01','总经理','1')  ;
INSERT INTO Port_Station (No,Name,StaGrade) VALUES('02','市场部经理','2')  ;
INSERT INTO Port_Station (No,Name,StaGrade) VALUES('03','研发部经理','2')  ;
INSERT INTO Port_Station (No,Name,StaGrade) VALUES('04','客服部经理','2')  ;
INSERT INTO Port_Station (No,Name,StaGrade) VALUES('05','财务部经理','2')  ;
INSERT INTO Port_Station (No,Name,StaGrade) VALUES('06','人力资源部经理','2')  ;
INSERT INTO Port_Station (No,Name,StaGrade) VALUES('07','销售人员岗','3')  ;
INSERT INTO Port_Station (No,Name,StaGrade) VALUES('08','程序员岗','3')  ;
INSERT INTO Port_Station (No,Name,StaGrade) VALUES('09','技术服务岗','3')  ;
INSERT INTO Port_Station (No,Name,StaGrade) VALUES('10','出纳岗','3')  ;
INSERT INTO Port_Station (No,Name,StaGrade) VALUES('11','人力资源助理岗','3')  ;

-- Port_Emp ;
-- 总经理部 ;
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('admin','admin','pub','01')  ;
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('zhoupeng','周朋','pub','01')  ;

-- 市场部 ;
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('zhanghaicheng','张海成','pub','0101')  ;
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('zhangyifan','张一帆','pub','0101')  ;
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('zhoushengyu','周升雨','pub','0101')  ;

-- 研发部 ;
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('qifenglin','祁凤林','pub','0102')  ;
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('zhoutianjiao','周天娇','pub','0102')  ;

-- 服务部经理 ;
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('guoxiangbin','郭祥斌','pub','0103')  ;
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('fuhui','福惠','pub','0103')  ;

-- 财务部 ;
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('yangyilei','杨依雷','pub','0104')  ;
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('guobaogeng','郭宝庚','pub','0104') ;

-- 人力资源部 ;
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('liping','李萍','pub','0105')  ;
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('liyan','李言','pub','0105')  ;

 
-- Port_EmpDept 人员与部门的对应 ;
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('zhoupeng','01') ;

-- 市场部 ;
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('zhanghaicheng','0101') ;
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('zhangyifan','0101') ;
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('zhoushengyu','0101') ;

-- 研发部 ;
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('qifenglin','0102') ;
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('zhoutianjiao','0102') ;

-- 服务部经理 ;
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('guoxiangbin','0103') ;
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('fuhui','0103') ;

-- 财务部 ;
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('yangyilei','0104') ;
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('guobaogeng','0104') ;

-- 人力资源部 ;
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('liping','0105') ;
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('liyan','0105') ;

-- Port_EmpStation 人员与岗位的对应 ;
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('zhoupeng','01')  ;

-- 市场部;
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('zhanghaicheng','02')  ;
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('zhangyifan','07')  ;
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('zhoushengyu','07')  ;

-- 研发部 ;
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('qifenglin','03')  ;
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('zhoutianjiao','08')  ;

--服务部;
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('guoxiangbin','04');
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('fuhui','09') ; 


-- 财务部;
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('yangyilei','05')   ;
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('guobaogeng','10')  ;

-- 人和资源部;
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('liping','06')  ;
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('liyan','11')  ;
 