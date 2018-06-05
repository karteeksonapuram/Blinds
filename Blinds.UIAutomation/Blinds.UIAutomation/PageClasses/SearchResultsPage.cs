using System;
using System.Collections.Generic;
using System.Linq;
using Blinds.UIAutomation.CommonClasses;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TechTalk.SpecFlow;

namespace Blinds.UIAutomation.PageClasses
{
    
    public class SearchResultsPage : CommonMethods
    {
        [FindsBy(How = How.CssSelector, Using = "div.shop-by-header-content")]
        public IWebElement ShopHeader { get; set; }

        [FindsBy(How = How.Id, Using = "gcc-search-filter")]
        public IWebElement VerticalSearchFilter { get; set; }

        [FindsBy(How = How.CssSelector, Using = "ul.list.m0.pr2-m.pr3-l")]
        public IList<IWebElement> VerticalSearchFilterComponents { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div.dn.db-ns.f7.f6-l")]
        public IWebElement SortBy { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div.pa3.w-100.w-50-ns.flex.flex-wrap.flex-column")]
        public IList<IWebElement> ProductSearchResults { get; set; }

        [FindsBy(How = How.Id, Using = "see-more-products-btn")]
        public IWebElement ShowMoreProductsButton { get; set; }

        public string SearchoptionCssselector = "li.mb2";

        public string SortoptionCssselector = "li.dib.ph2";

        public string ProductPriceCssselector = "div.f4.black.fw7";

        public IList<IWebElement> TotalSortOptions;

        readonly Dictionary<string, string> _productSearchResultsDetails = new Dictionary<string, string>();
        readonly Dictionary<string, Double> _productSearchPriceDetails = new Dictionary<string, Double>();
        public SearchResultsPage(IWebDriver browser)
            : base(browser)
        {

        }

        public void VerifyValidSearchResultsPageHasBeenDisplayed(string searchString)
        {
            if (ShopHeader.Text.ToLower().Contains(searchString.ToLower()))
            {
                Reporter.Pass("VerifyValidSearchResultsPageHasBeenDisplayed", searchString + " search page displayed.");
            }
            else
            {
                Reporter.Fail("VerifyValidSearchResultsPageHasBeenDisplayed", searchString + " search page is not displayed.");
            }
        }

        public void VerifyVerticalFilterOptionsHasBeenDisplayed(string options)
        {
            try
            {
                int i = 0;
                string[] optionsList = options.Split('|');
                if (VerticalSearchFilter.DoesExistsOnPage())
                {
                    if (VerticalSearchFilterComponents.Any())
                    {
                        foreach (IWebElement filterType in VerticalSearchFilterComponents)
                        {
                            
                            if (filterType.Text.ToLower().Contains(optionsList[i]))
                            {
                                Reporter.Pass("VerifyVerticalFilterOptionsHasBeenDisplayed", "Filter options are displayed as expected.");
                            }
                            else
                            {
                                Reporter.Fail("VerifyVerticalFilterOptionsHasBeenDisplayed", "Filteroptions are not proper.");
                            }
                            i++;
                        }
                    }
                }
            }
            catch(WebDriverException e)
            {
                Reporter.Fail("VerifyVerticalFilterOptionsHasBeenDisplayed", e.Message);
            }
        }

        public void VerifySortbyOptions()
        {
            if (SortBy.DoesExistsOnPage())
            {
                IList<IWebElement> totalSortOptions = SortBy.FindElements(By.CssSelector(SortoptionCssselector));
                TotalSortOptions = totalSortOptions;
            }
            if (TotalSortOptions.Any())
            {
                foreach (IWebElement sortOption in TotalSortOptions)
                {
                    Reporter.Pass("VerifySortbyOptions -" + sortOption.Text + " available", sortOption.Text);
                }
            }
            else
            {
                Reporter.Fail("VerifySortbyOptions", "Sort options are not available.");
            }
        }

        public void SelectSortByOption(string selectionCriteria)
        {
            foreach (IWebElement sortOption in TotalSortOptions)
            {
                if (sortOption.Text.ToLower().Contains(selectionCriteria.ToLower()))
                {
                    sortOption.Click();
                    Reporter.Pass("SelectSortByOption", selectionCriteria + " option selected.");
                    WaitForJqueryAjax();
                    break;
                }
            }
        }

        public string VerifyLowPriccedProductDisplayedOnTheTop()
        {
            int index = 0;
            VerifyProductAttributes();
            Dictionary<string, double> sortedProductSearchPriceDetails =
            _productSearchPriceDetails.OrderBy(x => x.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            foreach (var item in _productSearchPriceDetails)
            {
                if (item.Value != sortedProductSearchPriceDetails.ElementAtOrDefault(index).Value)
                {
                    Reporter.Fail("VerifyLowPriccedProductDisplayedOnTheTop","Sorting results are not proper. some elements are not sorted low-high");
                }
                index++;
            }
            //fetch first product after sorting.
            string productIdtoSelect;
            int i = -1;
            do
            {
                i++;
                productIdtoSelect = _productSearchPriceDetails.ElementAtOrDefault(i).Key;
            } while (_productSearchPriceDetails.ElementAtOrDefault(i).Value == -1);

            OfferId = productIdtoSelect;
            return productIdtoSelect;
        }

        private void VerifyProductAttributes()
        {
            try
            {
                if (ProductSearchResults.Any())
                {
                    while (ShowMoreProductsButton.DoesExistsOnPage())
                    {
                        ShowMoreProductsButton.Click();
                        WaitForJqueryAjax();
                    }

                    foreach (IWebElement product in ProductSearchResults)
                    {
                        string productId = product.GetAttribute("data-productid");
                        string productPrice = product.FindElement(By.CssSelector(ProductPriceCssselector)).Text;

                        string productDetails = product.Text;
                        productDetails = productDetails.Replace("\r\n", "|");
                        _productSearchResultsDetails.Add(productId, productDetails);
                        //Sometimes search results returns N/A as first productunder search results,but the price in description page is high
                        //it is unclear to me about this functionality,hence not considering n/a product.
                        if (productPrice.ToLower().Contains("n/a"))
                        {
                            _productSearchPriceDetails.Add(productId, -1);
                        }
                        else
                        {
                            productPrice = productPrice.Replace("$", "");
                            if (productPrice.ToLower().Contains("shipping"))
                            {
                                productPrice = productPrice.Replace("\r\nFree Shipping", "");
                            }
                            _productSearchPriceDetails.Add(productId, Convert.ToDouble(productPrice));
                        }
                    }
                }
            }
            catch(WebDriverException e)
            {
                Reporter.Fail("VerifyProductAttributes", e.Message);
            }
        }

        public void AddProductTocart(string productIdToSelect)
        {
            try
            {
                GetRequiredOfferElement(productIdToSelect).Click();
                ScenarioContext.Current.Set<ProductDescriptionPage>(new ProductDescriptionPage(Browser));
                WaitForPageLoad();
                Reporter.Pass("AddProductTocart", productIdToSelect+ ": added to cart successfully");
            }
            catch(WebDriverException e)
            {
                Reporter.Fail("AddProductTocart", e.Message);
            }

        }
        private IWebElement GetRequiredOfferElement(string offerId)
        {
            foreach (IWebElement product in ProductSearchResults)
            {
                if (product.GetAttribute("data-productid").Contains(offerId))
                {
                    return product;
                }
            }
            return null;
        }
    }
}

