using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandBookRepository;
using BrandBookModel;
namespace BrandBookBiz
{
    public class BrandBookFacadeBiz
    {
        #region private property
        StatusRepository _statusRepository;
        CommentRepository _commentRepository;
        LikeRepository _likeRepository;
        #endregion
        public BrandBookFacadeBiz()
        {
            _statusRepository = new StatusRepository();
        }
        public List<StatusUpdateModel> GetStatusList(StatusUpdateModel statusUpdateModel)
        {
            return _statusRepository.GetStatusList(statusUpdateModel);
        }
        public StatusUpdateModel SaveStatus(StatusUpdateModel statusUpdateModel)
        {
            return _statusRepository.SaveStatus(statusUpdateModel);
        }
        public CommentModel SaveComment(CommentModel commentModel)
        {
            return _commentRepository.SaveComment(commentModel);
        }
        public LikeModel SaveLike(LikeModel likeModel)
        {
            return _likeRepository.SaveLike(likeModel);
        }
    }
}
