use brandbook

go

create table UserDetails
(
	UserDetailsID int identity primary key,
	UserId uniqueidentifier,
	FirstName varchar(50),
	LastName varchar(50),
	Address varchar(300),
	City varchar(10),
	Country varchar(10),
	Designation varchar(20),
	Phone varchar(20),
	DOB date,
	Gender varchar(6),
	proPicId varchar(10),
	createdDate datetime default getdate()
)

go
 
 
create table StatusUpdate
(
	StatusID int identity primary key,
	UserDetailsID int,
	StatusType varchar(10),
	StatusContent varchar(500),
	createdDate datetime default getdate()
)
go
create table Comments
(
	CommentID int identity primary key,
	StatusID int,
	CommentedByID int,
	CommentType varchar(10),
	CommentContent varchar(500),
	CreatedDate datetime default getdate()
)
go
create table Likes
(
   LikeID int identity primary key,
   LikedContentType char(1),
   LikedContentId int,
   LikedByUserId int,
   CreatedDate datetime default getdate()
)
go
create table Images
(
	ImageID int identity primary key,
	UserDetailsID int ,
	ImageUrl varchar(200),
	CreatedDate datetime default getdate()
)

go

create table Videos
(
	VideoID int identity primary key,
	UserDetailsID int ,
	VideoUrl varchar(200),
	CreatedDate datetime default getdate()
)

