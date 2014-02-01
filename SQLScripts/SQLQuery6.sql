use brandbook
go

create table Brands
(
	BrandID int identity primary key,
	BrandName varchar(50),
	BrandDesc varchar(200)
)
create table UserBrands
(
	UserDetailsID int,
	BrandID int
)
go
create table Projects
(
	ProjectID int identity primary key,
	ProjectName varchar(50),
	ProjectDesc varchar(200)
)

go
create table UserProjects
(
	UserDetailsID int,
	ProjectID int
)


ALTER proc [dbo].[sp_getUserProfile]
(
@UserDetailsID int
)
as

select * from UserDetails
where UserDetailsID=@UserDetailsID

select su.StatusID as StatusID,
su.StatusType as StatusType,
su.StatusContent as StatusContent,
'' as fileDesc,
su.createdDate as CreatedDate,
su.UserDetailsID as StatusByUserID,
ud.FirstName+' '+ud.LastName as FullName,
ud.proPicId as PicID,
img.ImageUrl as ProPicUrl from StatusUpdate as su
join
UserDetails as ud
on su.UserDetailsID=ud.UserDetailsID
join Images as img
on ud.proPicId=img.ImageID

where su.StatusType='T'
union 
select 
temp.StatusID as StatusID,
temp.StatusType as StatusType,
temp.ImageUrl as StatusContent,
temp.ImgDesc as fileDesc,
temp.createdDate as CreatedDate,
temp.UserDetailsID as StatusByUserID,
temp.FirstName+' '+temp.LastName as FullName,
temp.proPicId as PicID,
img2.ImageUrl as ProPicUrl
 from 
(select su.StatusID,su.StatusType,img1.ImageUrl,img1.ImgDesc,su.createdDate,su.UserDetailsID,ud.FirstName,ud.LastName,ud.proPicId
from StatusUpdate as su
join
Images as img1
on su.StatusContent=img1.ImageID
join
UserDetails as ud
on su.UserDetailsID=ud.UserDetailsID
where su.StatusType='I') as temp
join Images as img2
on temp.proPicId=img2.ImageID


union all

select 
UserStatus.StatusID as StatusID,
UserStatus.StatusType as StatusType,
UserStatus.VideoUrl as StatusContent,
'' as fileDesc,
UserStatus.createdDate as CreatedDate,
UserStatus.UserDetailsID as StatusByUserID,
UserStatus.FirstName+' '+UserStatus.LastName as FullName,
UserStatus.proPicId as PicID,
img2.ImageUrl as ProPicUrl
 from 
(select su.StatusID,su.StatusType,vid1.VideoUrl,su.createdDate,su.UserDetailsID,ud.FirstName,ud.LastName,ud.proPicId
from StatusUpdate as su
join
UserDetails as ud
on su.UserDetailsID=ud.UserDetailsID
join
Videos as vid1
on su.StatusContent=vid1.VideoID
where su.StatusType='V') as UserStatus
join Images as img2
on UserStatus.proPicId=img2.ImageID
where UserStatus.UserDetailsID=@UserDetailsID
order by su.createdDate desc 


select 
cmt.StatusID as StatusID,
cmt.CommentID as CommentID,
cmt.CommentContent as CommentContent,
cmt.CreatedDate as CreatedDate,
cmt.CommentType as CommentType,
cmt.CommentedByID as CommentedByUserID,
ud.FirstName+' '+ud.LastName as CommentedByUserFullName,
ud.proPicId as CommentedByUserProPicID,
img.ImageUrl as CommentedByUserProPicUrl
 from 
Comments as cmt
join 
UserDetails as ud
on cmt.CommentedByID=ud.UserDetailsID
join
Images as img
on ud.proPicId=img.ImageID
join
StatusUpdate su
on
cmt.StatusID=su.StatusID
join
UserDetails 
on 
su.UserDetailsID=UserDetails.UserDetailsID
where UserDetails.UserDetailsID=@UserDetailsID
/*******************All Likes***********************/

select 
L.LikeID as LikeID,
L.LikedByUserId as LikedByUserID,
ud.FirstName+' '+ud.LastName as LikedByUserFullName,
L.LikedContentId as LikedContentID,
L.LikedContentType as LikedContentType,
L.CreatedDate as CreatedDate
from
Likes as L
join
UserDetails as ud
on L.LikedByUserId=ud.UserDetailsID
join
StatusUpdate as su
on su.StatusID=L.LikedContentId
join UserDetails as usr
on
su.UserDetailsID=usr.UserDetailsID
where usr.UserDetailsID=@UserDetailsID

/****************All brands**********/

select * from UserBrands ub
join
Brands b
on ub.BrandID=b.BrandID
where ub.UserDetailsID=@UserDetailsID

/****************All projects**********/

select * from UserProjects up
join
Projects p
on up.ProjectID=p.ProjectID
where up.UserDetailsID=@UserDetailsID