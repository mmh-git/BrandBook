use brandbook

go

ALTER PROC sp_SaveDeleteImage
(
	@ImageID int,
	@UserDetailsID int,
	@ImageUrl varchar(200),
	@ImgDesc nvarchar(500),
	@Action varchar(1)
)
AS

IF @Action='I'
	BEGIN
		INSERT INTO Images(UserDetailsID,ImageUrl,ImgDesc,CreatedDate)
		VALUES(@UserDetailsID,@ImageUrl,@ImgDesc,GETDATE())
		SELECT @ImageID=@@IDENTITY
	END
ELSE IF @Action='D'
	BEGIN
		DELETE Images WHERE ImageID=@ImageID
	END
SELECT * FROM Images WHERE ImageID=@ImageID