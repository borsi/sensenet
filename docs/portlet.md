|             |
|-------------|
| \_\_TOC\_\_ |

Overview
========

A Portlet is an ASP.NET web control that appear as a box on pages. Portlets can display custom layouts and implement custom application logic. The most simple portlets are used to present [Content](Content "wikilink") and [Content Collections](Content_Collection "wikilink"). Sense/Net Pages are mostly built up of Portlets - they are the basic building blocks of [Pages](Pages "wikilink").

Details
=======

Building custom pages is done by placing various types of portlets in the page layout. A portlet can be a custom web application (for example the [Login Portlet](Login_Portlet "wikilink") that accepts credentials of users and logs them into the portal) or portlets that rely much on the CMS features of Sense/Net to present Content or Content Collections.

#### List of built-in portlets

There are a couple of pre-defined portlets in the portal. These include the most basic applications and portlets that are necessary when building pages upon the [Content Repository](Content_Repository "wikilink"). Browse the full list of built-in portlets in the Table of Contents:

-   [List of Portlets](Table_of_Contents#Portlets "wikilink")

#### Adding portlets to a page

Portlets can easily be added to pages after editing the page and clicking on the *Add portlet* link placed at the top side of the different portlet zones. When clicked, a [Content Picker](Content_Picker "wikilink") will pop up and the portlet can be selected from pre-defined categories.

Added portlets are only visible to the public when the page has been checked-in (and when [versioning or approval](Versioning_and_approval "wikilink") of the page is switched on it is published/approved). Pages can be edited using the [Portal Remote Control](Portal_Remote_Control "wikilink") (PRC). After added the properties of the portlets can be set to customize the look and behavior of the portlet.

#### Editing portlet properties

Portlets can be customized via their properties. Properties can be accessed and set in the portlet properties dialog displayed after clicking *Edit* in the dropdown box that appear at the top right corner of the portlets when the page containing the portlet is in *Edit mode*. In the portlet properties dialog properties are organized into tabs along function categories.

Common properties include the ones displayed on the above screenshot:

-   **Portlet title**: the text displayed in the header of the portlet. Localizable resources can also be used, when the title is used in the form of *&lt;%$ Resources:YourResourceClassName, YourResourceName %&gt;*. See [Localization](Localization "wikilink") for details.
-   **Appearance**: controls the visibility of the border and the title (Default/TitleAndBorder/None/TitleOnly/BorderOnly, Default is both are visible)
-   **Renderer**: when the portlet supports XSLT rendering an [XSLT Renderer](XSLT_Renderer "wikilink") can be provided
-   **Custom CSS class(es)**: extra css classes to be rendered into the container of the portlet for easy customization of portlet look & feel

Displayed properties depend on the type of the portlet.

#### Cacheable portlets

Some portlets support caching functionality. When a portlet is *Cacheable* a *Cache* tab will appear on the portlet properties dialog. When caching enabled the portlet will cache its output for the given interval and thus when rendered no background logic will be executed within the cache interval. With this technique page response time can be lowered significantly. To read more about cacheable portlets and cache parameter settings please refer to the following page:

-   [Cacheable Portlet](Cacheable_Portlet "wikilink")

#### Context bound portlets

Some portlets support context binding functionality. This means that the functionality of the portlet depends on the Content being bound to the portlet. Specifying the Content (also referred to as the context) on which the portlet will execute its custom logic can be adjusted via properties of the *Context binding* tab that appears in the portlet properties dialog for context bound portlets. These portlets are the basic building blocks for creating [Smart Pages](Smart_Pages "wikilink") - pages that are able to present a whole set of Content with the same layout and function but changing content. Always use Context bound portlets to take fulladvantage of the [Smart Application Model](Smart_Application_Model "wikilink"). Context bound portlets can also be used for one specific Content instead of a class of Content. The properties to fine adjust context binding are the following:

-   **Bind Target**: this parameter specifies the base target of context binding. Most common choices include:
    -   CurrentContent - the context content itself. Makes it possible to use the same page and portlet for different Content.
    -   CurrentWorkspace - the closest ancestor Workspace of the current content.
    -   CurrentSite - the Site used to access the system.
    -   CurrentPage - The Smart Page itself. If you are using a Portlet Page as primary content, this is the same as *CurrentContent*.
    -   CurrentUser - The user who is logged in.
    -   CustomRoot - automatic Context Binding is disabled, and the Portlet is bound to a specific content item with an absolute path.
-   **Custom root path**: when *Bind Target* is set to *CustomRoot* the path of the specific Content can be given here.
-   **Ancestor selector index**: selects the ancestor of the set *Bind Target*. 0 leaves the bound content as specified above, 1 selects parent, higher value selects higher order ancestor.
-   **Relative content selector path**: sets the bound content relative to the above settings with a relative path. Ie.: *CustomChildFolder/CustomNode* selects *CustomNode* from *CustomChildFolder* child folder of the *Bind Target*. Given path is relative to ancestor when *Ancestor* selector index is different from 0.

Context bound portlets are [cachable portlets](Cacheable_Portlet "wikilink") by design.

#### Portlet inventory

Every Portlet is placed in the */Root/Portlets* folder as a Content of [Portlet Content Type](Portlet_Content_Type "wikilink"). This makes it possible for the users to adjust the behavior of the *add portlet* dialog. Portlet Content are organized into categories. Portlets and categories can easily be managed in [Explore](Explore "wikilink") as they build up a simple 1-level tree folder structure. Administrators can move portlets from one category to another, fine adjust Portlet visibility permissions for page editors or even delete Portlet Content so that it does not appear in the *add portlet* dialog. After deleted, you can always reinstall any portlets using the Synchronize Application (read on!).

#### Installing portlets

Only those portlets are visible in the *add portlet* dialog that are available in the Portlet inventory. Therefore when a new Portlet is created and added in the webfolder in the form of a dll file it has to be installed to the [Content Repository](Content_Repository "wikilink"). This can be done either by manually creating a new [Portlet](Portlet_Content_Type "wikilink") Content under the */Root/Portlets* folder in the appropriate Category folder, or automatically using the [Synchronize Application](Synchronize_Application "wikilink"). This latter is defined as an [Action](Action "wikilink") link on the */Root/Portlets* folder.

Example/Tutorials
=================

Related links
=============

-   [List of Portlets](Table_of_Contents#Portlets "wikilink")
-   [Smart Pages](Smart_Pages "wikilink")
-   [Synchronize Application](Synchronize_Application "wikilink")
-   [How to add a portlet to a page](How_to_add_a_portlet_to_a_page "wikilink")
-   [How to create a simple Portlet Page](How_to_create_a_simple_Portlet_Page "wikilink")
-   [How to create and install a Portlet](How_to_create_and_install_a_Portlet "wikilink")

References
==========

[Category:End users](Category:End_users "wikilink") [Category:Portal builders](Category:Portal_builders "wikilink")
