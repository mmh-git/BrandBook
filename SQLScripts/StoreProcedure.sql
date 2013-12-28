use brandbook

go
/**********All Status************/
create proc statusUpdate_getStatusList
as
select su.StatusID as StatusID,
su.StatusType as StatusType,
su.StatusContent as StatusContent,
su.createdDate as CreatedDate,
su.UserDetailsID as StatusByUserID,
ud.FirstName+' '+ud.LastName as FullName,
ud.proPicId as PicID,
img.ImageUrl as ProPicUrl from StatusUpdate as su
join
UserDetails as ud
on su.UserDetailsID=ud.UserDetailsID
join Images as img
on ud.UserDetailsID=img.UserDetailsID
where su.StatusType='T'

union all
select su.StatusID as StatusID,
su.StatusType as StatusType,
img1.ImageUrl as StatusContent,
su.createdDate as CreatedDate,
su.UserDetailsID as StatusByUserID,
ud.FirstName+' '+ud.LastName as FullName,
ud.proPicId as PicID,
img2.ImageUrl as ProPicUrl from StatusUpdate as su
join
Images as img1
on su.StatusContent=img1.ImageID
join
UserDetails as ud
on su.UserDetailsID=ud.UserDetailsID
join Images as img2
on ud.UserDetailsID=img2.UserDetailsID
where su.StatusType='I'
union all

select su.StatusID as StatusID,
su.StatusType as StatusType,
vdo.VideoUrl as StatusContent,
su.createdDate as CreatedDate,
su.UserDetailsID as StatusByUserID,
ud.FirstName+' '+ud.LastName as FullName,
ud.proPicId as PicId,
img2.ImageUrl as ProPicUrl from StatusUpdate as su
join
Videos as vdo
on su.StatusContent=vdo.VideoUrl
join
UserDetails as ud
on su.UserDetailsID=ud.UserDetailsID
join Images as img2
on ud.UserDetailsID=img2.UserDetailsID
where su.StatusType='V'

/*************All Comments*****************/

select 
cmt.StatusID as StatusID,
cmt.CommentID as CommentID,
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
on cmt.CommentedByID=img.UserDetailsID
/*******************All Likes***********************/

select 
L.LikeID as LikeID,
L.LikedByUserId as LikedByUserID,
ud.FirstName+' '+ud.LastName as LikedByUserFullName,
L.LikedContentId as LikeContentID,
L.LikedContentType as LikedContentType
from
Likes as L
join
UserDetails as ud
on L.LikedByUserId=ud.UserDetailsID