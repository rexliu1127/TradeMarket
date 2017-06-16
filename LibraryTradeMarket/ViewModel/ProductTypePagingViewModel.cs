using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTradeMarket
{
    public class ProductTypePagingViewModel
    {

        private int currentPage;
        private int totalPage;
        private int totalCount;
        private int showCount;



        public int CurrentPage { get => currentPage; set => currentPage = value; }
        public int TotalPage { get => totalPage; set => totalPage = value; }
        public int TotalCount { get => totalCount; set => totalCount = value; }
        public int ShowCount { get => showCount; set => showCount = value; }



        public List<ProductTypeViewModel> ListOfProductType;

        public ProductTypePagingViewModel()
        {
            ListOfProductType = new List<ProductTypeViewModel>();
            CurrentPage = 1;
            TotalPage = 1;
            TotalCount = 0;
            ShowCount = 10;
        }
    }
}
