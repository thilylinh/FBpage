using Model.DAO;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Project5_trangdocbao.Areas.Admin.Models
{
    public class NewRSS
    {
        private XmlDocument _rss = null;

        public struct RssChannel
        {
            public string Title;
            public string Link;
            public string Description;
            public string LastBuildDate;
            public string PubDate;
            public string Guid;
            public string Author;
            public string Content;

        }


        public struct RssItem
        {
            public string Title;
            public string Link;
            public string Description;
            public string PubDate;
            public string Guid;
            public string Author;
            public string Content;
            public string LastBuildDate;

        }

        private static XmlDocument addRssChannel(XmlDocument xmlDocument, RssChannel channel)
        {
            XmlElement channelElement = xmlDocument.CreateElement("channel");
            XmlNode rssElement = xmlDocument.SelectSingleNode("rss");
            rssElement.AppendChild(channelElement);
            XmlElement titleElement = xmlDocument.CreateElement("title");
            titleElement.InnerText = channel.Title;
            channelElement.AppendChild(titleElement);
            XmlElement linkElement = xmlDocument.CreateElement("link");
            linkElement.InnerText = channel.Link;
            channelElement.AppendChild(linkElement);
            XmlElement descriptionElement = xmlDocument.CreateElement("description");
            descriptionElement.InnerText = channel.Description;
            channelElement.AppendChild(descriptionElement);

            XmlElement lastBuildDate = xmlDocument.CreateElement("lastBuildDate");
            lastBuildDate.InnerText = channel.LastBuildDate;
            channelElement.AppendChild(lastBuildDate);

            // Generator information
            //XmlElement generatorElement = xmlDocument.CreateElement("generator");
            //generatorElement.InnerText = "Your RSS Generator name and version ";
            //channelElement.AppendChild(generatorElement);
            return xmlDocument;
        }

        private static XmlDocument addRssItem(XmlDocument xmlDocument, RssItem item)
        {
            XmlElement itemElement = xmlDocument.CreateElement("item");
            XmlNode channelElement = xmlDocument.SelectSingleNode("rss/channel");

            XmlElement titleElement = xmlDocument.CreateElement("title");
            titleElement.InnerText = item.Title;
            itemElement.AppendChild(titleElement);

            XmlElement linkElement = xmlDocument.CreateElement("link");
            linkElement.InnerText = item.Link;
            itemElement.AppendChild(linkElement);

            XmlElement guid = xmlDocument.CreateElement("guid");
            guid.InnerText = item.Guid;
            itemElement.AppendChild(guid);

            XmlElement pubDate = xmlDocument.CreateElement("pubDate");
            pubDate.InnerText = item.PubDate;
            itemElement.AppendChild(pubDate);

            XmlElement author = xmlDocument.CreateElement("author");
            author.InnerText = item.Author;
            itemElement.AppendChild(author);

            XmlElement descriptionElement = xmlDocument.CreateElement("description");
            descriptionElement.InnerText = item.Description;
            itemElement.AppendChild(descriptionElement);



            XmlElement content = xmlDocument.CreateElement("content:encoded");
            content.InnerText = item.Content;
            itemElement.AppendChild(content);

            // append the item
            channelElement.AppendChild(itemElement);
            return xmlDocument;
        }

        public NewRSS()
        {
            _rss = new XmlDocument();
            XmlDeclaration xmlDeclaration = _rss.CreateXmlDeclaration("1.0", "utf-8", null);
            _rss.InsertBefore(xmlDeclaration, _rss.DocumentElement);

            XmlElement rssElement = _rss.CreateElement("rss");
            XmlAttribute rssVersionAttribute = _rss.CreateAttribute("version");

            rssVersionAttribute.InnerText = "2.0";
            rssElement.Attributes.Append(rssVersionAttribute);

            XmlAttribute xmls = _rss.CreateAttribute("xmlns:content");
            xmls.InnerText = "http://purl.org/rss/1.0/modules/content/";
            rssElement.Attributes.Append(xmls);

            _rss.AppendChild(rssElement);

        }

        public void AddRssChannel(RssChannel channel)
        {
            _rss = addRssChannel(_rss, channel);
        }

        public void AddRssItem(RssItem item)
        {
            _rss = addRssItem(_rss, item);
        }

        public string RssDocument
        {
            get
            {
                return _rss.OuterXml;
            }
        }

        public XmlDocument RssXMLDocument
        {
            get
            {
                return _rss;
            }
        }

        public static void LoadRSS()
        {
            string link = "https://news24tube.com";
            var path = HttpContext.Current.Server.MapPath("~");
            NewRSS rss = new NewRSS();
            NewRSS.RssChannel channel = new NewRSS.RssChannel();
            channel.Title = "Website Chia sẻ Tin tức";
            channel.Link = "https://news24tube.com";
            channel.Description = "Chuyên đào tạo và Phát triển công nghệ phần mềm";
            channel.LastBuildDate = DateTime.Now.Date.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.sssZ");

            //channel.
            rss.AddRssChannel(channel);
            NewRSS.RssItem item = new NewRSS.RssItem();

            var dao = new PostDao();
            var modelFirst = dao.ListAll("", 1, 15);
            var model = new List<BAIDANG>();
            foreach (var item2 in modelFirst)
            {
                if (item2.NgayDang.Value.Date == DateTime.Now.Date)
                    model.Add(item2);
            }
            if (model.Count() > 0)
            {
                //Phần liệt kê
                foreach (var bd in model)
                {

                    item.Title = bd.TenBaiDang;
                    item.Link = "https://news24tube.com" + "/Doc-bao/" + bd.UrlRequire + "-" + bd.IDBaiDang;
                    item.Guid = item.Link;
                    if (bd.NgayDang != null)
                        item.PubDate = bd.NgayDang.Value.Date.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.sssZ");
                    else
                    {
                        item.PubDate = DateTime.Now.Date.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.sssZ");
                    }
                    item.Author = bd.TAIKHOAN.HoTen;
                    item.Description = bd.TieuDe;
                    string head = "<html prefix=\"op: http://media.facebook.com/op#\"><head>"
                    + "<link rel=\"canonical\" href=\"" + item.Link + "\"/>"
                        //+ "<meta charset=\"utf-8\"/><meta property=\"op:generator\" content=\"facebook-instant-articles-sdk-php\"/>"
                        + "<meta charset=\"utf-8\"/>"
                        + "<meta property=\"op:markup_version\" content=\"v1.0\"/>"
                        //+ "<meta property=\"fb:use_automatic_ad_placement\" content=\"enable=true ad_density=default\">"
                        + "<meta property=\"fb:op-recirculation-ads\" content=\"enable=true placement_id=175618137823132_175618157823130\"/>"
                                       //+ "<meta property=\"op:generator:version\" content=\"1.10.0\"/>"
                                       //+ "<meta property=\"op:generator:application\" content=\"facebook-instant-articles-wp\"/>"
                                       //+ "<meta property=\"op:generator:application:version\" content=\"4.2.1\"/><meta property=\"op:generator:transformer\" content=\"facebook-instant-articles-sdk-php\"/>"
                                       //+ "<meta property=\"op:generator:transformer:version\" content=\"1.10.0\"/><meta property=\"op:markup_version\" content=\"v1.0\"/>"
                                       //+ "<meta property=\"fb:use_automatic_ad_placement\" content=\"enable=true ad_density=default\"/><meta property=\"fb:article_style\" content=\"default\"/>"
                                       + "</head><body><article>"
                        + ""
                        + ""
                        + "";
                    string header = "<header>"
                        + "<figure><img src=\"" + link + bd.AnhDaiDien + "\"/></figure>"
                        + "<h1>" + bd.TenBaiDang + "r</h1>"
                        + "<time class=\"op - published\" datetime=\"" + item.PubDate + "\">" + bd.NgayDang.Value.ToString("dd:MM:yyyy") + "</time>"
                        + "<address><a>" + bd.TAIKHOAN?.HoTen + "</a></address>"
                        + "<h2 class=\"op - kicker\">" + bd.TieuDe + "</h2>"
                        //+ "<figure class=\"op-ad\"><iframe src=\"https://www.facebook.com/adnw_request?placement=175618137823132_175618157823130&adtype=banner300x250\" width=\"300\" height=\"250\"></iframe></figure>"
                      + "</header>";
                    string footer = "<footer><p>Copyright 2020 Tạ Khánh Thiện</p></footer>";
                    string headend = "</article></body></html>";
                    string quangcao = "<figure class=\"op - tracker\"><iframe><script> (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){ (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o), m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m) })(window,document,'script','https://www.google-analytics.com/analytics.js','ga'); ga('create', 'UA-195329056-1', 'auto'); ga('require', 'displayfeatures'); ga('set', 'campaignSource', 'Facebook'); ga('set', 'campaignMedium', 'Social Instant Article'); ga('set', 'title', 'IA: '+ia_document.title); ga('send', 'pageview');</script></iframe></figure>";
                    item.Content = "<![CDATA[<!doctype html>" + head + header + bd.NoiDung + footer + headend + "]]>";
                    rss.AddRssItem(item);
                }
                var t = rss.RssDocument.Replace("<encoded>", "<content:encoded>").Replace("</encoded>", "</content:encoded>");
                System.IO.File.WriteAllText(path + "feed/instant-articles.xml", t);
            }
            
            //Response.Clear();
            //    Response.ContentType = "text/xml";
            //    Response.Write(rss.RssDocument);
            //    Response.End();

        }

    }
}