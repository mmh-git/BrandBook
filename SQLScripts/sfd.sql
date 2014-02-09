alter proc sp_editUserInfo
(
@UserDetailsID int,
@FirstName varchar(50),
@LastName varchar(50),
@Address varchar(300),
@Designation varchar(50),
@Mobile varchar(20),
@Extension varchar(10)
)

as
--set rowcount 0
update UserDetails set FirstName=@FirstName,
LastName=@LastName,
Address=@Address,
Designation=@Designation,
Mobile=@Mobile,
Extension=@Extension
where UserDetailsID=@UserDetailsID
--return @@rowcount