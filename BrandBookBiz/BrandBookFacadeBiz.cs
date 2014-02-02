using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandBookModel;
using BrandBookRepository;
namespace BrandBookBiz
{
   public class BrandBookFacadeBiz
    {
        #region private property
        StatusRepository _statusRepository;
        CommentRepository _commentRepository;
        LikeRepository _likeRepository;
        UserRepository _userRepository;
        ImageRepository _imageRepository;
        UserProfileRepository _userProfileRepository;
        #endregion
        public BrandBookFacadeBiz()
        {
            _statusRepository = new StatusRepository();
            _commentRepository = new CommentRepository();
            _likeRepository = new LikeRepository();
            _userRepository = new UserRepository();
            _imageRepository = new ImageRepository();
            _userProfileRepository = new UserProfileRepository();
        }
        #region Status Method
        public List<StatusUpdateModel> GetStatusList(StatusUpdateModel statusUpdateModel)
        {
            return _statusRepository.GetStatusList(statusUpdateModel);
        }
        public StatusUpdateModel SaveStatus(StatusUpdateModel statusUpdateModel)
        {
            return _statusRepository.SaveStatus(statusUpdateModel);
        }
        #endregion
        #region Comment Method
        public CommentModel SaveComment(CommentModel commentModel)
        {
            return _commentRepository.SaveComment(commentModel);
        }
        #endregion
        #region Like Method
        public List<LikeModel> SaveLike(LikeModel likeModel)
        {
            return _likeRepository.SaveLike(likeModel);
        }
        #endregion
        #region User Method

        public UserModel GetUserDetails(UserModel userModel)
        {
            return _userRepository.GetUserDetails(userModel);
        }
        public UserModel SaveUserDetails(UserModel userModel)
        {
            return _userRepository.SaveUserDetails(userModel);
        }
        #endregion
        #region Image Method
        public ImageModel SaveImage(ImageModel imageModel)
        {
            return _imageRepository.SaveImage(imageModel);
        }
        #endregion
        #region UserProfile Method
        public UserProfile GetUserProfile(UserModel userModel)
        {
            return _userProfileRepository.GetUserProfile(userModel);
        }
        #endregion
    }
}
