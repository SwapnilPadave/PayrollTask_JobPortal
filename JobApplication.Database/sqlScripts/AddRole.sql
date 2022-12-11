Create Table [User]
(
Id int primary key identity(1,1),
[Name] nvarchar(100),
[Password] nvarchar(Max),
Email nvarchar(100),
RoleId int,
CreatedDate datetime,
ModifiedDate datetime,
IsActive bit
)

Create Table Job
(
Id int primary key identity(1,1),
Title nvarchar(100),
[Description] nvarchar(200),
CreatedBy int,
CreatedAt datetime,
isActive bit
)

Create Table Candidate
(
Id int primary key identity(1,1),
CandidateId int,
AppliedJobId int,
AppliedAt datetime
)

Create Table Otp
(
Id int primary key identity(1,1),
Otp int,
expiry datetime,
CreateDate datetime,
GenerateBy int
)

Create Table [Role]
(
Id int primary key identity(1,1),
[Role] varchar(20) 
)

Create Table RoleMappingModel
(
Id int primary key identity(1,1),
RoleId int,
UserId int
)

