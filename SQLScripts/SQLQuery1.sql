use brandbook
go
create proc sp_getBrandList
(
@UserDetailsID int
)

as

select  b.BrandID,b.BrandName,b.BrandDesc from Brands b
join UserBrands ub
on b.BrandID=ub.BrandID
where ub.UserDetailsID=@UserDetailsID
go

create proc sp_getProjectList
(
@UserDetailsID int
)

as

select  b.ProjectID,b.ProjectName,b.ProjectDesc from Projects b
join UserProjects ub
on b.ProjectID=ub.ProjectID
where ub.UserDetailsID=@UserDetailsID

alter proc sp_insertBrands
(
@userDetailsId int,
@brandid int output,
@brandName varchar(max),
@brandDesc varchar(200)
)
as

if @brandid !=null or @brandid !=0
begin
	update brands set brandName=@brandName
	where brandid=@brandid
end
else
begin
	insert into Brands(BrandName,BrandDesc)
	values(@brandName,@brandDesc)
	select @brandid=@@IDENTITY

	insert into UserBrands(BrandID,UserDetailsID)
	values(@brandid,@userDetailsId)
end
alter proc sp_insertProjects
(
@userDetailsId int,
@projectid int output,
@projectName varchar(max),
@projectDesc varchar(200)
)
as
if @projectid !=null or @projectid !=0
begin
	update Projects set ProjectName=@projectName
	where ProjectID=@projectid
end
else
begin
insert into Projects(ProjectName,ProjectDesc)
values(@projectName,@projectDesc)
select @projectid=@@IDENTITY

insert into UserProjects(ProjectID,UserDetailsID)
values(@projectid,@userDetailsId)
end



delete Brands
delete Projects
delete UserBrands
delete UserProjects