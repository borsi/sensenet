Overview
========

The Smart Application model is a feature of Sense/Net for defining frontend templates to display and manipulate data in the Repository. It enables portal builders to create a unique look and feel across content items while also saving time.

Basic philosophy
================

The classic approach in **ASP.NET WebForms** is to create **Forms** ([Pages](Portlet_Page "wikilink") in Sense/Net) that will aggregate and display data based on their programmed logic. When you enter an address (eg. <http://example.com/Default.aspx>) into your browser's location bar, it will be the address of a Form. This is pretty straightforward from a programmer's viewpoint. You create logic - web applications - that are explicitly called and executed. However, from a user's viewpoint, nothing can be farther from being straightforward.

Users are usually interested in content items (blogposts, videos, images, etc.), not the logic that can display them. The classic approach seen across the web is to request the desired content in a parameter, usually through the URL query string. This results in URLs like this:

-   <http://www.example.com/Engine.aspx?board=52&item=4329&mode=show>

Such URLs are not friendly to either users or search engines. The most prominent place in the URL - basically, the URL itself - is taken by the web application, which should be a quiet servant to the content.

This approach is similar to starting a word processor from the Start Menu, and then opening a document from inside the software. A more intuitive way to work is to find the document you wish to edit, and double-click it. The operating system will automatically load the word processor, and open the document in it. And that is exactly what the **Smart Application Model** does in Sense/Net.

Here, you have URLs like the following:

-   <http://www.example.com/MyBlog/2010/08/Great_day>

Where “Great\_day” is an actual content item (a blog post) located under the path “*MyBlog/2010/08/Great\_day*”. Now, somewhere in the system there exists a [Portlet Page](Portlet_Page "wikilink"), an ASP.NET application, that will display this blog post. We have simply hidden it from the eyes of the user, and put the content item itself into the lead role.

Addressing a piece of content
=============================

Data in the Sense/Net Content Repository is stored in a tree model, similar to a file system. Each item has a unique content path through which it can be accessed. However, as a single instance of Sense/Net can serve several sites with different content, we provide two URL schemes for addressing content items from the web.

Site relative paths
-------------------

The most common URLs used in a Sense/Net installation are *site relative*, sometimes referred to as *friendly URLs*. This means exactly what it says. Sites are content items themselves in the Sense/Net Content Repository, and a relative path points to content that is located inside the site it is accessed from.

For example, let's say we have the following setup:

-   Root
    -   Sites
        -   Exemplar\_Cars (*<http://cars.example.com/>*)
            -   Intro
            -   Buy
            -   ...
        -   Exemplar\_Bikes (*<http://bikes.example.com/>*)
            -   Intro
            -   Buy
            -   ...
    -   Data
        -   Prices
            -   pricesheet\_2009
            -   pricesheet\_2010

Usually, people will access this repository through one of the sites defined, the **Cars** or the **Bikes** site. Normally whatever follows the domain name in the URL will be the relative path of a content item under the site accessed.

-   <http://cars.example.com/Buy> points to **/Root/Sites/Exemplar\_Cars/Buy**
-   <http://bikes.example.com/Buy> points to **/Root/Sites/Exemplar\_Bikes/Buy**

Absolute paths
--------------

Absolute paths, sometimes also referred to as *“root relative paths”* are fully qualified paths in the local part of the URL. With absolute paths, you can access content that is not physically located under the site you are logged in to. For a local part to be interpreted as an absolute path, it must start with **/Root**. Usually this is used for administration work, as eg. users and groups, or global resources such as icons and JavaScript frameworks are located outside any of the sites.

Examples:

-   <http://cars.example.com/Root/Data/Prices/pricesheet_2009> points to **/Root/Data/Prices/pricesheet\_2009**
-   <http://bikes.example.com/Root/Data/Prices/pricesheet_2009> points to **/Root/Data/Prices/pricesheet\_2009**
-   <http://cars.example.com/Root/Sites/Exemplar_Bikes/Buy> points to **/Root/Sites/Exemplar\_Bikes/Buy**

Actions
=======

Addressing a content item is only half of the equation, you also need to say what you want to do with it. There are various operations one might execute on a piece of data.

In Sense/Net, what you want to do with a content item is called an **Action**, and can be passed via the “*action*” query string parameter:

-   <http://www.example.com/MyBlog/2010/08/Great_day?action=Edit>

If a content item is requested without an action, it is equivalent to specifying the default action, which is **Browse**.

Sense/Net will load a different application depending on the action specified, therefore you can have a [page](Portlet_Page "wikilink") for displaying a blog post to readers, one for editing it, etc. There is no limitation to the possible number and nature of Actions, so you can, and are encouraged to create entirely custom actions specific to your needs.

Possible actions may, as an example, include:

-   <http://www.example.com/MyBlog/2010/08?action=NewEntry>
-   <http://www.example.com/MyForum/MyTopic?action=Moderate>
-   <http://www.example.com/StrategyGame/Cloud_Kingdom?action=Attack>
-   <http://www.example.com/Software/Sales?action=Executive_BI_Dashboard>

Application binding
===================

The **Smart Applicaton Model** goes further than just providing a friendly URL scheme. The real power lies in the complex logic that binds applications to content.

In Sense/Net, everything is content, and this includes the applications themselves. This means that the applications are either located in the [Content Repository](Content_Repository "wikilink") as physical [Portlet Pages](Portlet_Page "wikilink") or ASP.NET webforms, or are placeholder content items for logic (like HTTP Handlers) located in a DLL file. (See [Smart Application Model for Developers](Smart_Application_Model_for_Developers "wikilink") to read more on using HTTP Handlers as applications.)

Registering an application consists of placing it at the right place in the [Content Repository](Content_Repository "wikilink"), and naming it appropriately.

Binding by action
-----------------

The way applications are resolved from the Action specified in the URL is as straightforward as it gets. The application needs to be named the same as the Action. Eg. default applications must be called **Browse**, an application for **Edit** actions must be called **Edit**, etc.

Binding by location
-------------------

Applications are located in a special [System Folder](System_Folder "wikilink") called *(apps)*.

There can be an *(apps)* folder anywhere in the system, and when a content item is requested, the system finds the *(apps)* folder that is the closest, and looks at the applications that have been placed there.

If an appropriate application isn't found, the system checks the second closest *(apps)* folder, and so on, until an application is found.

Binding by type
---------------

Like an operating system, Sense/Net also has [types](Content_Type "wikilink"). The first layer of the binding logic does the same thing that your favorite OS would do to open the word processor for a document. It finds an application registered for the [Content Type](Content_Type "wikilink") of the requested item.

In fact, it goes one step further, as Sense/Net types have inheritance. Therefore, if an application isn't found for the type in question, the system will check each of its ancestors, until a registered app is found.

To registed an application for a specific [Content Type](Content_Type "wikilink"), you need to place it into a folder named after the type under an *(apps)* folder. Therefore, the path of a **Browse** application registered for the **HtmlContent** type may look something like this:

-   *{ something }*/(apps)/**HtmlContent**/**Browse**

“This” binding
--------------

Similar to binding by type, a set of Smart Applications can be defined for a single content item, and that single content item only, by placing the Smart Applications into a folder named **This** in the *(apps)* folder under the content item in question:

-   *{ my content item }*/(apps)/**This**/**Browse**

The Smart Applications defined with a This binding have absolute priority in the resolution process.

Seeing it as a whole
--------------------

Above we have looked at the three components of application binding. However, to work efficiently, you need to understand how they relate to each other.

The simplest part of the process is the action binding. The system will look for an application that is named exactly as the **Action** specified. There are no fallbacks on this, if no application is found, an error message will be returned.

When a request arrives, the system will sequentially probe the *(apps)* system folders starting from the closest to the content item requested. In each *(apps)* folder, it will look for a folder that matches the **type** of the requested content item most closely, while containing an application with the same name as the **Action** specified. If not found, it advances on to the next *(apps)* folder, until a matching application is found, or it becomes apparent that such an application is not present.

### Examples

See the following, simple content tree:

-   MySite
    -   (apps)
        -   GenericContent
            -   Browse
            -   Edit
        -   BlogPost
            -   Browse
    -   SpecialSection
        -   (apps)
            -   GenericContent
                -   Browse
        -   SpecialBlog
            -   Post1 *(type: BlogPost)*
            -   Post2 *(type: BlogPost)*
    -   Blog
        -   Post1 *(type: BlogPost)*
        -   Post2 *(type: BlogPost)*
            -   (apps)
                -   This
                    -   Browse
        -   Post3 *(type: BlogPost)*

Let's have a look at some requests that may arrive, and how the system handles them:

-   <http://www.example.com/Blog/Post1>

  
Here we have no **Action** specified, which means that the default Action will be used, which is **Browse**. This is how the request will be resolved:

:\# No *(apps)* folder found under **Blog**

:\# Found *(apps)* folder under the parent folder (**MySite**)

:\# **Browse** application found under the folder **BlogPost**

:\# Application used: **/MySite/(apps)/BlogPost/Browse**

-   <http://www.example.com/Blog/Post2>

:\# *(apps)* folder found under **Post2**

:\# *This* folder found under **Post2/(apps)**

:\# **Browse** application found under the folder **This**

:\# Application used: **/MySite/Blog/Post2/(apps)/This/Browse**

  
Note that the Application placed in the *This* folder under the Content (*Post2*) itself overrides the Application used in the case of *Post1*

-   <http://www.example.com/Blog/Post1?action=Edit>

:\# No *(apps)* folder found under **Blog**

:\# Found *(apps)* folder under the parent folder (**MySite**)

:\# No **Edit** application found under the folder **BlogPost**

:\# Checking folders matching the ancestors of the **BlogPost** type

:\# **Edit** application found under the folder **GenericContent**

:\# Application used: **/MySite/(apps)/GenericContent/Edit**

-   <http://www.example.com/SpecialSection/SpecialBlog/Post1>

  
Again, no action specified means **Browse** action.

:\# No *(apps)* folder found under **SpecialBlog**

:\# Found *(apps)* folder under the parent folder (**SpecialSection**)

:\# Checking folders matching the ancestors of the **BlogPost** type

:\# **Browse** application found under the folder **GenericContent**

:\# Application used: **/MySite/SpecialSection/(apps)/GenericContent/Browse**

  
Note that though there is a specific **Browse** application for BlogPosts, the GenericContent application placed in a higher position in the tree overrides it.

-   <http://www.example.com/SpecialSection/SpecialBlog/Post1?action=Edit>

:\# No *(apps)* folder found under **SpecialBlog**

:\# Found *(apps)* folder under the parent folder (**SpecialSection**)

:\# No **Edit** application found under any folder matching an ancestor of **BlogPost**. Moving on to next *(apps)* folder.

:\# Found *(apps)* folder under the parent folder (**MySite**)

:\# No **Edit** application found under the folder **BlogPost**

:\# Checking folders matching the ancestors of the **BlogPost** type

:\# **Edit** application found under the folder **GenericContent**

:\# Application used: **/MySite/(apps)/GenericContent/Edit**

Self-dispatching content
========================

There is a way to override the above mechanism on a single content item. This is possible by setting the **Default browse application** field to an executable content item ([Portlet Page](Portlet_Page "wikilink"), Webform, HttpHandler, etc.)

If set, the system will always display the content item using this application in **Browse** mode.

Pages as content
================

Before the introduction of the Smart Application Model, the preferred building block of Sites were [Portlet Pages](Portlet_Page "wikilink"). URLs would always point at Portlet Pages or downloadable Files, with preset configurations or query string parameters specifying the content item(s) to be displayed.

You must know that this is still an option. [Portlet Pages](Portlet_Pages "wikilink") can, and in certain circumstances, should, be used as primary content items to be directly addressed by the user. To help you decide when to do this, we have collected some disadvantages of the Smart Application Model, and select scenarios when addressing [Portlet Pages](Portlet_Pages "wikilink") directly may be the best thing to do.

There is, however, a golden middle. By creating a **Browse** Smart Application with “This” binding, you can have the best of both worlds. This comes in especially handy when you need a unique, dashboard-like Portlet Page to be displayed on a major container node, such as a Site root or a [Workspace](Workspace "wikilink"). Simply put, use [Portlet Pages](Portlet_Pages "wikilink") for leaf content and “This” binding for containers.

Smart Application Model disadvantages
-------------------------------------

-   The presentations of specific content items aren't readily customizable
-   On production systems it may not always be clear which Smart Application is used and why
-   When making changes to a Smart Application, the scope of affected content is not always obvious
-   Sometimes the Smart Application philosophy does not fit the goal (eg. as with a calculator or on-line tool of some sort)

When to use Portlet Pages as primary content
--------------------------------------------

-   For stand-alone tools and applications (like the calculator mentioned before)
-   For leaf dashboard pages that aggregate content from all around the system (eg. BI dashboards)
-   To display content physically outside the Site without using an absolute URL
-   To display data from external sources (eg. web services)
-   When creating an AJAX-heavy browsing experience that encompasses several content items
-   When a Smart Application is unlikely to ever be invoked for more than one single content item

Permissions
===========

The applications accessible to the user depends on permissions set on the applications and on the content itself. For more details on which permissions you should set on the applications and on the content, please visit the following article:

-   [Application permission settings](Application#Permission_settings "wikilink")

Related pages
=============

-   [Smart Pages](Smart_Pages "wikilink")
-   [Action](Action "wikilink")
-   [Application](Application "wikilink")

<Category:Features> [Category:Portal builders](Category:Portal_builders "wikilink") <Category:Developers>
