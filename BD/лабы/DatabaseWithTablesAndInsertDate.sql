USE master;
DROP DATABASE IF EXISTS Shutro_MyBase;
GO
CREATE DATABASE Shutro_MyBase on primary
( name = N'Shutro_MyBase_ndf', filename = N'D:\BD\Shutro_MyBase_ndf.ndf', 
 size = 10240KB, maxsize=1Gb, filegrowth=25%),
filegroup FG1
( name = N'Shutro_MyBase_fg1_1', filename = N'D:\BD\Shutro_MyBase_fgq-1.ndf',
 size = 10240Kb, maxsize=1Gb, filegrowth=25%),
( name = N'Shutro_MyBase_fg1_2', filename = N'D:\BD\Shutro_MyBase_fgq-2.ndf',
 size = 10240Kb, maxsize=1Gb, filegrowth=25%)
log on
( name = N'Shutro_MyBase_log', filename=N'D:\BD\Shutro_MyBase_log.ldf',
 size=10240Kb, maxsize=2Gb, filegrowth=25%);
 GO  
USE Shutro_MyBase;
GO
CREATE TABLE groups
(groupId bigint PRIMARY KEY,
groupName nvarchar(2)) on FG1;
CREATE TABLE faculty
(facultyId bigint PRIMARY KEY,
facultyName nvarchar(30) NOT NULL,
) on FG1;
CREATE TABLE departments
(departmentId bigint PRIMARY KEY,
departmentName nvarchar(100) NOT NULL,
facultyId  bigint FOREIGN KEY REFERENCES faculty(facultyId) NOT NULL
) on FG1;
CREATE TABLE students
(studentId bigint PRIMARY KEY,
lastName nvarchar(30) NOT NULL,
firstName nvarchar(20) NOT NULL,
middleName nvarchar(30) NULL,
studentAddress nvarchar(30) NOT NULL,
birthdayDay date NOT NULL,
phone nvarchar(15) NOT NULL,
groupId bigint FOREIGN KEY REFERENCES groups(groupId) NOT NULL) on FG1;
CREATE TABLE studies
(subjectId bigint PRIMARY KEY,
subjectName nvarchar(30) NOT NULL,
facultyId bigint FOREIGN KEY REFERENCES faculty(facultyId) NOT NULL,
lecturesVolume int NOT NULL,
practiceVolume int NOT NULL,
labsVolume int NOT NULL) on FG1;
CREATE TABLE marks
(markId bigint PRIMARY KEY,
studentId bigint FOREIGN KEY REFERENCES students(studentId) NOT NULL,
subjectId bigint FOREIGN KEY REFERENCES studies(subjectId) NOT NULL,
mark int NOT NULL) on FG1;
ALTER TABLE marks ADD endDate date NOT NULL;
ALTER TABLE students ADD gender nchar(1) default 'm' check (gender in ('m','f'));
CREATE TABLE auditore
(auditoreId bigint PRIMARY KEY,
auditoreNumber int NOT NULL UNIQUE ,
numberOfPlaces int,
auditoreTypeId bigint FOREIGN KEY REFERENCES auditore(auditoreId) NOT NULL) on FG1;
CREATE TABLE auditoreType(
auditoreTypeId bigint PRIMARY KEY,
typeOfRoom nvarchar(20))on FG1;
CREATE TABLE teacher
(teacherId bigint PRIMARY KEY,
gender nvarchar(1) NOT NULL,
teacherName nvarchar(60) NOT NULL) on FG1;
CREATE TABLE timeTable
(timeTableId bigint PRIMARY KEY,
groupId bigint FOREIGN KEY REFERENCES groups(groupId) NOT NULL,
auditoreId bigint FOREIGN KEY REFERENCES auditore(auditoreId) NOT NULL,
subjectId bigint FOREIGN KEY REFERENCES studies(subjectId) NOT NULL,
teacherId bigint FOREIGN KEY REFERENCES teacher(teacherId) NOT NULL,
dayNames nvarchar(30),
lesson int) on FG1;
set nocount on;
INSERT INTO groups(groupId,groupName) VALUES
(1, 'G1'),
(2,	'G2');
INSERT INTO faculty(facultyId,facultyName) VALUES
(1, 'mehmat'),
(2,	'chimfac'),
(3,'geofac'),
(4,'sportfac');
INSERT INTO departments(departmentId,departmentName,facultyId) VALUES
(1,'Department of Web-Technologies and Computer Modeling',1),
(2,'The Mathematical Cybernetics Department',1),
(3,'Department of Theoretical and Applied Mechanics',1),
(4,'Department of analytical chemistry',2),
(5,'Department of Electrochemistry',2),
(6,'Department of organic chemistry',2),
(7,'Department of General Earth Science',3),
(8,'Department of Geodesy and Cosmoaerocartography',3),
(9,'Department of Economic and Social Geography',3),
(10,'Department of sports disciplines and methods of their teaching',4),
(11,'Department of Athletics, Swimming and Skiing',4);
INSERT INTO students(studentId,lastName,firstName,middleName,studentAddress,birthdayDay,phone,groupId,gender) VALUES
(1,'Ivanov', 'Ivan', 'Ivanovich', 'Rew St.','09-09-2003', '+3752545678',1,'m'),
(2,'Petrov', 'Petr', 'Petrovich', 'Ter St.','09-09-2003', '+3754567890',2,'m'),
(3,'Sydrov', 'Sydr', 'Sydrovich', 'Qer St.','11-09-2003', '+3756543748',1,'m'),
(4,'Ali', 'Muhammad',NULL, 'Per St.','09-11-2003', '+3756547382',1,'m'),
(5,'Silverhand', 'Jonny', NULL,	'Ser St.','10-11-2003', '+37533875642',2,'m'),
(6,'Peter', 'Cruz', NULL,	'Beverly St.','07-10-2003', '+37544872162',2,'m');
INSERT INTO studies(subjectId,facultyId, subjectName, lecturesVolume, practiceVolume, labsVolume) VALUES
(1, 1, 'math', 50, 15, 10),
(2,	2, 'chemistry', 30, 15, 10),
(3, 3, 'geography', 16, 8, 6),
(4,	4, 'boxing', 50, 20, 10),
(5, 4, 'shooting', 60, 40, 20),
(6, 1, 'high math', 50, 15, 10),
(7, 1, 'discrete math', 60, 40, 20),
(8, 1, 'casino math', 26, 16, 18),
(9, 4, 'swimming', 60, 20, 0),
(10, 2, 'analytical chemistry', 60, 40, 20),
(11, 4, 'inorganic chemistry', 56, 50, 30);
INSERT INTO marks(markId, studentId, subjectId,	mark,endDate) VALUES
(1,	1,	1,	10,'09-09-2019'),
(2,	1,	3,	10,'09-09-2019'),
(3,	1,	2,	5,'09-09-2019'),
(4,	2,	5,	3,'09-09-2019'),
(5,	2,	3,	9,'09-09-2019'),
(6,	3,	6,	7,'09-09-2019'),
(7,	3,	4,	9,'09-09-2019'),
(8,	3,	1,	6,'09-09-2019'),
(9,	4,	7,	9,'09-09-2019'),
(10,4,	2,	4,'09-09-2019'),
(11,4,	5,	4,'09-09-2019'),
(12,5,	5,	10,'09-09-2019'),
(13,5,	6,	7,'09-09-2019'),
(14,5,	1,	9,'09-09-2019'),
(15,4,	2,	5,'09-09-2019'),
(16,4,	4,	4,'09-09-2019'),
(17,5,	2,	6,'09-09-2019'),
(18,5,	6,	6,'09-09-2019'),
(19,5,	7,	8,'09-09-2019'),
(20,5,	3,	2,'09-09-2019');
INSERT INTO auditoreType(auditoreTypeId,typeOfRoom) VALUES 
(1,'practice'),
(2,'lecture'),
(3,'lab');
INSERT INTO auditore(auditoreId,auditoreNumber,numberOfPlaces,auditoreTypeId) VALUES
(1,150,30,1),
(2,140,50,1),
(3,160,200,2),
(4,180,10,3),
(5,200,40,2),
(6,360,190,2),
(7,270,20,3),
(8,460,250,2),
(9,470,15,3);
INSERT INTO teacher(teacherId,teacherName,gender) VALUES
(1, 'Albert Einstein','m'),
(2, 'Diego','m'),
(3, 'Magaradji','m'),
(4, 'Albus','m');
INSERT INTO timeTable(timeTableId,groupId,auditoreId,subjectId,teacherId,dayNames,lesson) VALUES
(1,1,1,1,1,'Mon',1),
(2,2,3,1,1,'Mon',2),
(3,2,1,2,2,'Tue',1),
(4,1,2,1,3,'Tue',2),
(5,1,1,2,1,'Tue',3),
(6,2,2,1,4,'Wed',1),
(7,2,3,2,2,'Wed',2),
(8,1,1,1,2,'Thu',1),
(9,2,3,2,4,'Fri',1),
(10,1,1,1,3,'Fri',2);


 --12
 DELETE auditore WHERE auditoreId IN(10,11,12,13,17)
 DELETE students WHERE studentId IN (7,8,9,10,13,14,15);
 GO
 set nocount on
if exists (select * from SYS.OBJECTS 
 where OBJECT_ID= object_id(N'DBO.X') )
DROP TABLE X;
declare @c INT, @flag CHAR = 'c'; 
SET IMPLICIT_TRANSACTIONS ON 
CREATE TABLE X(K int ); 
INSERT X VALUES (1),(2),(3);
SET @c = (SELECT count(*) from X);
PRINT N'количество строк в таблице X: ' + cast( @c as varchar(2));
if @flag = 'c' commit; 
 else rollback; 
 SET IMPLICIT_TRANSACTIONS OFF
if exists (select * from SYS.OBJECTS 
 where OBJECT_ID= object_id(N'DBO.X') )
print N'table X is exist';
 else print N'table X is not exist'

 GO
BEGIN TRY
BEGIN TRAN
INSERT INTO students VALUES(7,'Grag','Dalton', NULL,'Retra St.','10-11-2003', '+37525835632',1,'m');
INSERT INTO students VALUES(8,'Harry', 'Henderson', NULL,'Btr St.','03-10-2003', '+37533801132',2,'m');
COMMIT TRAN;
END TRY
BEGIN CATCH
PRINT 'ERROR'+ CASE
WHEN ERROR_NUMBER() = 2627 AND PATINDEX('%_students',ERROR_MESSAGE())>0
THEN 'STUDENT IS ALREADY EXIST'
ELSE 'UNKNOWN ERROR' + CAST(ERROR_NUMBER() AS NVARCHAR(5)) + ERROR_MESSAGE()
END;
IF @@TRANCOUNT > 0 ROLLBACK TRAN;
END CATCH;



DECLARE @point VARCHAR(32);
BEGIN TRY
BEGIN TRAN
DELETE students WHERE studentId = 7;
SET @point = 'p1';
SAVE TRAN @point;
INSERT students VALUES(9,'Derril','Bagatov', NULL,'Retra St.','10-07-2003', '+37525835632',1,'m');
SET @point = 'p2';
SAVE TRAN @point;
INSERT students VALUES(10,'Nick','Yawin', NULL,'Liza St.','10-12-2004', '+37525335021',2,'m');
COMMIT TRAN
END TRY
BEGIN CATCH
PRINT 'ERROR'+ CASE
WHEN ERROR_NUMBER() = 2627 AND PATINDEX('%_students',ERROR_MESSAGE())>0
THEN 'STUDENT IS ALREADY EXIST'
ELSE 'UNKNOWN ERROR' + CAST(ERROR_NUMBER() AS NVARCHAR(5)) + ERROR_MESSAGE()
END;
IF @@TRANCOUNT > 0
BEGIN;

PRINT 'CONTROL POINT' + @point;
ROLLBACK TRAN @point;
COMMIT TRAN;
END;
END CATCH;



SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
BEGIN TRANSACTION
--A
SELECT @@SPID, 'insert auditore' 'result',* FROM auditore
 WHERE auditoreTypeId = 2;
 INSERT auditore VALUES (17,410,90,2);
 SELECT @@SPID, 'insert auditore' 'result',* FROM auditore
 WHERE auditoreTypeId = 2;

COMMIT;

--B
BEGIN TRANSACTION
SELECT @@SPID
INSERT auditore VALUES (10,461,90,2);
 SELECT @@SPID, 'insert auditore' 'result',* FROM auditore
 WHERE auditoreTypeId = 2;
ROLLBACK;


SET TRANSACTION ISOLATION LEVEL READ COMMITTED
BEGIN TRANSACTION
--A
SELECT @@SPID, 'insert auditore' 'result',* FROM auditore
 WHERE auditoreTypeId = 2;
 INSERT auditore VALUES (12,412,90,2);
 SELECT @@SPID, 'insert auditore' 'result',* FROM auditore
 WHERE auditoreTypeId = 2;
COMMIT;

--B
BEGIN TRANSACTION
SELECT @@SPID
INSERT auditore VALUES (10,466,90,2);
 SELECT @@SPID, 'insert auditore' 'result',* FROM auditore
 WHERE auditoreTypeId = 2;
ROLLBACK;

-- A ---
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
BEGIN TRANSACTION
SELECT COUNT(*) FROM students WHERE studentId = 6;

SELECT 'update students' 'result', COUNT(*)
 FROM students WHERE studentId = 6;
COMMIT;
--- B ---
BEGIN TRANSACTION

 UPDATE students SET firstName= 'Yarik'
 WHERE studentId = 6
 COMMIT;

 
 -- A ---
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
BEGIN TRANSACTION
SELECT firstName FROM students WHERE groupId = 2;

SELECT CASE
 WHEN groupId = 2 THEN 'insert student'
 ELSE ' '
END 'result', firstName FROM students WHERE firstName = 'Terry';
COMMIT
--- B ---
BEGIN TRANSACTION

 INSERT students VALUES(13,'Terry','Hanry', NULL,'Park St.','01-06-2003', '+37525838772',2,'m');
 COMMIT;

 
GO
 -- A ---
 SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
BEGIN TRANSACTION
DELETE students WHERE studentId = 12;
 INSERT students VALUES(12,'Garloon','Hanry', NULL,'Park St.','11-08-2004', '+37525838772',2,'m'); 
 SELECT firstName,lastName FROM students WHERE studentId = 12;
-------------------------- t1 ------------------
 SELECT firstName,lastName FROM students WHERE studentId = 12;
-------------------------- t2 ------------------
COMMIT;
--- B ---
BEGIN TRANSACTION
DELETE students WHERE studentId =12;
 INSERT students VALUES(12,'Garloon','Hanry', NULL,'Park St.','11-08-2004', '+37525838772',2,'m'); 
 SELECT firstName,lastName FROM students WHERE studentId = 12;
 -------------------------- t1 --------------------
 COMMIT;

 SELECT firstName,lastName FROM students WHERE studentId = 12;

 
 BEGIN TRAN
 INSERT students VALUES(15,'Garlon','Tod', NULL,'Park St.','01-02-2004', '+37525844752',2,'m');
 BEGIN TRAN
 INSERT students VALUES(14,'Thor','Hericlov', NULL,'Park St.','11-04-2003', '+37525838772',1,'m');
 COMMIT;
 IF @@TRANCOUNT > 0 ROLLBACK;
 SELECT COUNT(*) FROM students;


 