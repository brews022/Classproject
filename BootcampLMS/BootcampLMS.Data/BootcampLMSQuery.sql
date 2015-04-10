Use master
GO

if exists(select * from sysdatabases where name = 'BootcampLMS')
	drop database BootcampLMS
GO


create database BootcampLMS
GO

Use BootcampLMS
GO

Create Table [dbo].[UserProfile](
	[UserId] [nvarchar](128) NOT NULL Primary Key,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[RequestedRole] [nchar](15) NOT NULL,
	[GradeLevel] [int] NULL,
)

Create Table [dbo].[ParentStudent](
	[ParentId] [nvarchar](128) NOT NULL,
	[StudentId] [nvarchar](128) NOT NULL,
	Constraint PK_ParentStudent Primary Key(ParentId, StudentId)
)

Create Table [dbo].[Roster](
	[RosterId] [int] NOT NULL Primary Key IDENTITY(1,1),
	[UserId] [nvarchar](128) NOT NULL,
	[CourseId] [int] NOT NULL,
	[IsActive] [Bit] not null
)

Create Table [dbo].[Course](
	[CourseId] [int] NOT NULL Primary Key IDENTITY(1,1),
	[TeacherId] [nvarchar](128) NOT NULL,
	[Name] [nvarchar] (100) Not Null,
	[Department] [nvarchar] (50) Not Null,
	[CourseDescription] [nvarchar] (MAX) Not Null,
	[StartDate] [DateTime] Not Null,
	[EndDate] [DateTime] Not Null,
	[GradeLevel] [int] null,
	[IsArchived] [bit] not null
)

Create Table [dbo].[Assignment](
	[AssignmentId] [int] Not Null Primary Key IDENTITY(1,1),
	[CourseId] [int] Not Null,
	[Name] [nvarchar](100) not null,
	[Description] [nvarchar] (MAX) not Null,
	[DueDate] [DateTime] Not Null,
	[PointsPossible] [int] not null
)

Create Table [dbo].[AssignmentTracker](
	[Id] [int] Not Null Primary Key IDENTITY(1,1),
	[AssignmentId] [int] Not Null,
	[RosterId] [int] NOT NULL,
	[EarnedPoints] [decimal](10, 2) NOT NULL
)

Alter Table [dbo].[Roster]
	Add Constraint FK_Roster_UserProfile Foreign Key (UserId)
	references [dbo].[UserProfile] (UserId)

Alter Table [dbo].[Roster]
	Add Constraint FK_Roster_Course Foreign Key (CourseId)
	references [dbo].[Course] (CourseId)

Alter Table [dbo].[AssignmentTracker]
	Add Constraint FK_AssignmentTracker_Roster Foreign Key (RosterId)
	references [dbo].[Roster] (RosterId)

Alter Table [dbo].[AssignmentTracker]
	Add Constraint FK_AssignmentTracker_Assignment Foreign Key (AssignmentId)
	references [dbo].[Assignment] (AssignmentId)

Alter Table [dbo].[Assignment]
	Add Constraint FK_Assignment_Course Foreign Key (CourseId)
	references [dbo].[Course] (CourseId)

Alter Table [dbo].[Course]
	Add Constraint FK_Course_UserProfile Foreign Key (TeacherId)
	references [dbo].[UserProfile] (UserId)

Alter Table [dbo].[ParentStudent] Add
	Constraint FK_ParentStudent_UserProfile Foreign Key (ParentId)
	references [dbo].[UserProfile] (UserId)

Alter Table [dbo].[ParentStudent] Add
	Constraint FK_ParentStudent_UserProfile1 Foreign Key (StudentId)
	references [dbo].[UserProfile] (UserId)
