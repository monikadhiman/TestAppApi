create database testmgt_new;
use testmgt_new;



CREATE TABLE dbo.tblUsers
(
    ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
    UserID AS 'UID' + RIGHT('000' + CAST(ID AS VARCHAR(3)), 3) PERSISTED, 
    [Name] VARCHAR(50) NOT NULL,
)
-------------------------------------------------------------------------------
create table roleTab
(
roleId int not null identity (1,1) primary key,
roleName varchar(50)
);
create table courseTab
(
cId int identity (1,1) not null primary key,
cName varchar(50),
isActive bit
);
create table subjectTab
(
subId int identity (1,1) not null PRIMARY KEY,
subName varchar(3),
isActive bit,
cId int foreign key references coursetab(cId)
);
create table userTab
(
    uId INT IDENTITY(1,1) NOT NULL primary key,
    fullName varchar(50),
	email varchar(50) not null unique, 
	password varchar(50),
	roleId int foreign key references roleTab(roleId),
	isActive bit,
	
);
create table studentTab
(
	stuId int not null primary key,
	uid int not null foreign key references userTab(uId),
	cId int foreign key references coursetab(cId),
	constraint unique_Uid unique (uid)
);

create table assignSubTeacherTab
(
aId int identity (1,1) not null primary key,
uId int foreign key references userTab(uId),
subId int foreign key references subjectTab(subId)
);

create table testTab
(
tstId int identity (1,1) not null primary key,
aId int foreign key references assignSubTeacherTab(aId)
);
create table questionTab
(
quesId int identity (1,1) not null primary key,
ques varchar (max),
opt1 varchar(250),
opt2 varchar(250),
opt3 varchar(250),
answer varchar(250),
tstId int foreign key references testTab(tstId)
);
create table studentRemarkTab
(
rId int identity (1,1) not null primary key,
uId int foreign key references userTab(uId),
totalMarks int,
obtainedMarks int,
tstId int foreign key references testTab(tstId),
subId int foreign key references subjectTab(subId)
);
-------------------------------------------------------------------
new 
-------------------------------------------------------------------
create database testmgt_new;
use testmgt_new;



CREATE TABLE dbo.tblUsers
(
    ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
    UserID AS 'UID' + RIGHT('000' + CAST(ID AS VARCHAR(3)), 3) PERSISTED, 
    [Name] VARCHAR(50) NOT NULL,
)

create table studentTab
(
	stuId int not null primary key,
	uid int not null foreign key references userTab(uId),
	cId int foreign key references coursetab(cId),
	constraint unique_Uid unique (uid)
);
-------------------------------------------------------------------------------
create table roleTab
(
roleId int not null identity (1,1) primary key,
roleName varchar(50)
);
create table courseTab
(
cId int identity (1,1) not null primary key,
cName varchar(50),
isActive bit
);
create table subjectTab
(
subId int identity (1,1) not null PRIMARY KEY,
subName varchar(50),
isActive bit,
cId int foreign key references coursetab(cId)
);
create table userTab
(
    uId INT IDENTITY(1,1) NOT NULL primary key,
    fullName varchar(50),
	email varchar(50) not null unique, 
	password varchar(50),
	roleId int foreign key references roleTab(roleId),
	isActive bit,
	cId int foreign key references coursetab(cId),
);
create table assignSubTeacherTab
(
aId int identity (1,1) not null primary key,
uId int foreign key references userTab(uId),
subId int foreign key references subjectTab(subId),
isActive bit
);

create table testTab
(
tstId int identity (1,1) not null primary key,
tstName varchar(50),
cId int foreign key references coursetab(cId),
subId int foreign key references subjectTab(subId),
aId int foreign key references assignSubTeacherTab(aId)
);
create table questionTab
(
quesId int identity (1,1) not null primary key,
ques varchar (100),
opt1 varchar(100),
opt2 varchar(100),
opt3 varchar(100),
opt4 varchar(100),
answer int,
tstId int foreign key references testTab(tstId),
mark int 
);
drop table questionTab
create table answerTab
(
ansId int identity (1,1) primary key,
quesId int foreign key references questionTab(quesId),
answer int,
uId int foreign key references userTab(uId)
);

create table score
(
scoreId int identity (1,1) primary key,
email varchar(50),
score int,
timespend int,
tstId int foreign key references testTab(tstId)
);
drop table score
create table studentRemarkTab
(
rId int identity (1,1) not null primary key,
uId int foreign key references userTab(uId),
totalMarks int,
obtainedMarks int,
tstId int foreign key references testTab(tstId),
subId int foreign key references subjectTab(subId)
);

select * from courseTab where isActive=1
select * from usertab
update usertab set roleId=1 where uid= 1
select * from subjectTab
select * from userTab where roleId is null
SELECT       questionTab.ques AS Expr1, questionTab.quesId AS Expr2, questionTab.*, testTab.tstId AS Expr3, assignSubTeacherTab.uId
FROM            questionTab INNER JOIN
                         testTab ON questionTab.tstId = testTab.tstId INNER JOIN
                         assignSubTeacherTab ON testTab.aId = assignSubTeacherTab.aId


						 select * from sys.tables
select * from userTab;						 
select * from roleTab;
select * from courseTab;
select * from subjectTab
select * from assignSubTeacherTab;
select * from testTab;
select subId  from assignSubTeacherTab where uId=2
select subName from subjectTab where subId in (select subId  from assignSubTeacherTab where uId=2)
select cName from courseTab where exists (select * from subjectTab where subId in (select subId from assignSubTeacherTab where uId=2))
select subName from subjectTab where subId = any (select subId from assignSubTeacherTab where uId=2)
select subName from subjectTab where cId=2 
select * from assignSubTeacherTab where uId=2
select * from courseTab
SELECT regid,firstname,lastname,email,Subject.sname,Course.coursename,Registration.isActive FROM Registration,Subject,AssignSubject,Course where roleid=2 and AssignSubject.subjectidref=Subject.subjectid and Course.courseid=(select Subject.courseidref from Subject where Subject.subjectid=AssignSubject.subjectidref) and AssignSubject.regidref=regid

-------------------------------------
select distinct courseTab.cName from courseTab,subjectTab where courseTab.cId=subjectTab.cId and subId in (select assignSubTeacherTab.subId from assignSubTeacherTab where assignSubTeacherTab.uId=2);


select distinct Course.coursename from Course,Subject where courseid=Subject.courseidref and subjectid in (select AssignSubject.subjectidref from AssignSubject where AssignSubject.regidref=2);


select * from courseTab,subjectTab where courseTab.cId=1 and subjectTab.subId in (select assignSubTeacherTab.subId from assignSubTeacherTab where assignSubTeacherTab.uId=2);


select subjectTab.subName from subjectTab,courseTab where courseTab.cId=1 and subjectTab.subId in (select assignSubTeacherTab.subId from assignSubTeacherTab where assignSubTeacherTab.uId=2 and assignSubTeacherTab.subId in (select subjectTab.subId from subjectTab where cid=(select cId from courseTab where cId=1)))

 select Subject.sname from Subject,Course where courseid=4 and Subject.subjectid in (select AssignSubject.subjectidref from AssignSubject where AssignSubject.regidref=2 and AssignSubject.subjectidref in (select Subject.subjectid from Subject where courseidref=(select courseid from Course where courseid=4)))
 select * from courseTab

  SELECT  regid,firstname,lastname,email,Subject.sname,Course.coursename,Registration.isActive FROM Registration,Subject,AssignSubject,Course 
  select * from courseTab
  update subjectTab set isActive='1' where subId='1'
  select * from subjectTab
  select * from subjectTab;
  select * from courseTab;
  select subjectTab.subId,subjectTab.subName,subjectTab.isActive,subjectTab.cId,courseTab.cName from subjectTab,courseTab where subjectTab.cId=courseTab.cId
  select subName from subjectTab where cId=1 and subName='English'
  select * from subjectTab
  select * from subjectTab where 
 select * from userTab 
 update userTab set roleId=0 where uid=3
 SELECT  regid,firstname,lastname,email,Subject.sname,Course.coursename,Registration.isActive FROM Registration,Subject,AssignSubject,Course 
 select ai ,fullName, email, subjectTab.subName, courseTab.cName, userTab.isActive from userTab,subjectTab,assignSubTeacherTab,courseTab


 select * from userTab where roleId=2 and isActive=1
 select * from courseTab where isActive=1
 select * from subjectTab where cId=1 and isActive=1
 select * from userTab

select *from assignSubTeacherTab;
delete from assignSubTeacherTab where aId=2
select * from subjectTab;
select * from courseTab;
select * from userTab;
--select assignSubTeacherTab.aId,usertab.fullName,assignSubTeacherTab.isActive from assignSubTeacherTab, userTab where assignSubTeacherTab.uId = userTab.uId and 
select assignSubTeacherTab.aId,assignSubTeacherTab.uId, userTab.fullName, assignSubTeacherTab.subId, subjectTab.subName, courseTab.cId,courseTab.cName,assignSubTeacherTab.isActive from assignSubTeacherTab,userTab,courseTab,subjectTab where assignSubTeacherTab.uId=userTab.uId and assignSubTeacherTab.subId=subjectTab.subId and subjectTab.cId=courseTab.cId and userTab.email='jyoti@gmail.com' and userTab.roleId=2 and assignSubTeacherTab.isActive=1

SELECT * FROM USERTAB;
SELECT * FROM assignSubTeacherTab where uId=3 and subId=3 and isActive=1
select * from testtab
select * from questiontab
select * from usertab
truncate table usertab
truncate table assignSubTeacherTab

select 
select * from testtab
select * from questiontab
select * from assignSubTeacherTab where uId=3;
select * from subjectTab
select * from courseTab
select * from userTab
select * from subjectTab
select  count(courseTab.cId) as cId, courseTab.cName from assignSubTeacherTab,userTab,courseTab,subjectTab where assignSubTeacherTab.uId=userTab.uId and assignSubTeacherTab.subId=subjectTab.subId and subjectTab.cId=courseTab.cId and userTab.email='jyoti@gmail.com' and userTab.roleId=2 and assignSubTeacherTab.isActive=1  group by  courseTab.cName
select * from assignSubTeacherTab where uId=3
select * from subjectTab
select * from courseTab
select assignSubTeacherTab.subId, subjectTab.subName from assignSubTeacherTab,userTab, subjectTab where assignSubTeacherTab.subId=subjectTab.subId and assignSubTeacherTab.uId=userTab.uId and userTab.email='jyoti@gmail.com' and userTab.roleId=2 and assignSubTeacherTab.isActive=1
select * from testtab
select assignSubTeacherTab.aId from assignSubTeacherTab, userTab, subjectTab, courseTab where assignSubTeacherTab.uId = userTab.uId and assignSubTeacherTab.subId=subjectTab.subId and subjectTab.cId = courseTab.cId and userTab.email='jyoti@gmail.com' and assignSubTeacherTab.subId=3 and userTab.roleId=2 and assignSubTeacherTab.isActive =1
select assignSubTeacherTab.subId , subjectTab.subName from assignSubTeacherTab, userTab, subjectTab, courseTab where assignSubTeacherTab.uId = userTab.uId and assignSubTeacherTab.subId=subjectTab.subId and subjectTab.cId = courseTab.cId and userTab.email='jyoti@gmail.com' and courseTab.cId=1 and userTab.roleId=2 and assignSubTeacherTab.isActive =1 

select assignSubTeacherTab.aId,assignSubTeacherTab.uId, userTab.fullName, assignSubTeacherTab.subId, subjectTab.subName, courseTab.cId,courseTab.cName,assignSubTeacherTab.isActive from assignSubTeacherTab,userTab,courseTab,subjectTab where assignSubTeacherTab.uId=userTab.uId and assignSubTeacherTab.subId=subjectTab.subId and subjectTab.cId=courseTab.cId and userTab.email='jyoti@gmail.com' and courseTab.cId=2 and  userTab.roleId=2 and assignSubTeacherTab.isActive=1

select * from subjectTab
select * from courseTab
select  distinct courseTab.cId, courseTab.cName from assignSubTeacherTab,userTab,courseTab,subjectTab where assignSubTeacherTab.uId=userTab.uId and assignSubTeacherTab.subId=subjectTab.subId and subjectTab.cId=courseTab.cId and userTab.email='jyoti@gmail.com' and userTab.roleId=2 and assignSubTeacherTab.isActive=1  
select * from courseTab;
select * from subjectTab;
select * from testtab
select testtab.tstId, testtab.tstName, courseTab.cName, subjectTab.subName  from testtab, courseTab, subjectTab where testtab.subId=subjectTab.subId and testtab.cId=coursetab.cId
select * from questiontab
select * from testtab
select * from assignSubTeacherTab
select testtab.tstId, testtab.tstName, testtab.cid, testtab.subId, testtab.aId, usertab.email,usertab.isActive from testtab, assignSubTeacherTab,usertab where testtab.aId=assignSubTeacherTab.aId and assignSubTeacherTab.uId=usertab.uId and userTab.email='jyoti@gmail.com' and usertab.isActive=1
select * from questab;
select questiontab.quesId, questiontab.ques, questiontab.opt1, questiontab.opt2, questiontab.opt3, questiontab.opt4, questiontab.answer, testtab.tstName, questiontab.mark from questiontab, testtab,assignSubTeacherTab,usertab where questiontab.tstId=testTab.tstId and questiontab.tstId=testtab.tstId and testtab.aid = assignSubTeacherTab.aid and assignSubTeacherTab.uid=usertab.uId and usertab.email='jyoti@gmail.com' and usertab.isActive=1 and usertab.roleId=2
select * from testtab
select * from questiontab;
select * from answerTab;
select * from score;
select * from usertab;

select * from testtab
select * from questiontab;
select * from testtab;
select * from assignSubTeacherTab;
select * from userTab
select * FROM coursetab
select * FROM SUBJECTTAB
select cid from usertab where email='swati@gmail.com'
select questiontab.quesId, questiontab.ques, questiontab.opt1, questiontab.opt2, questiontab.opt3, questiontab.opt4, questiontab.answer, testTab.tstName, courseTab.cName, subjectTab.subName from questiontab, testtab, coursetab, subjectTab,userTab where questiontab.tstId=testtab.tstId and testtab.cid=courseTab.cid and testtab.subId=subjectTab.subId and usertab.email='swati@gmail.com'

select questiontab.quesId, questiontab.ques, questiontab.opt1, questiontab.opt2, questiontab.opt3, questiontab.opt4, questiontab.answer, testTab.tstName, courseTab.cName, subjectTab.subName from questiontab, testtab, coursetab, subjectTab,userTab where questiontab.tstId=testtab.tstId and testtab.cid=courseTab.cid and testtab.subId=subjectTab.subId and usertab.email='swati@gmail.com' and usertab.cId=1

select  coursetab.cId,courseTab.cName from questiontab, testtab, coursetab, subjectTab,userTab where questiontab.tstId=testtab.tstId and testtab.cid=courseTab.cid and testtab.subId=subjectTab.subId and usertab.email='swati@gmail.com' and usertab.cId=1

select  distinct subjectTab.subId,subjectTab.subName from questiontab, testtab, coursetab, subjectTab,userTab where questiontab.tstId=testtab.tstId and testtab.cid=courseTab.cid and testtab.subId=subjectTab.subId and usertab.email='swati@gmail.com' and courseTab.cId=1 

select questiontab.quesId, questiontab.ques, questiontab.opt1, questiontab.opt2, questiontab.opt3, questiontab.opt4, questiontab.answer, testTab.tstName, courseTab.cName, subjectTab.subName from questiontab, testtab, coursetab, subjectTab,userTab where questiontab.tstId=testtab.tstId and testtab.cid=courseTab.cid and testtab.subId=subjectTab.subId and usertab.email='swati@gmail.com' and testTab.cId=1 and testTab.subId=3
select * from answerTab;
select * from score 
select courseTab.cId, courseTab.cName from courseTab where cId=1

select * from questiontab
select * from score
truncate table score
truncate table questiontab
select * from sys.tables
truncate table testtab
truncate table courseTab
select * from score where email='swati@gmail.com' and tstId = 1
select * from score;
truncate table score
select * from questiontab
select * from usertab;
select * from assignSubTeacherTab;
select * from subjectTab
select * from testTab
select * from testTab;
select * from assignSubTeacherTab;
select * from userTab;
select * from courseTab;
select * from subjectTab;


select testTab.tstId, testTab.tstName, courseTab.cId, courseTab.cName, subjectTab.subId, subjectTab.subName from testTab, courseTab, subjectTab, assignSubTeacherTab, userTab where testTab.cId = courseTab.cId and testTab.subId = subjectTab.subId and testTab.aId = assignSubTeacherTab.aId and assignSubTeacherTab.uId = userTab.uId and userTab.email = 'rohit@gmail.com'
select * from score
truncate table score
