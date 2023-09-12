# blog-template
![Build](https://img.shields.io/github/actions/workflow/status/sh1ngekyo/blog-template/build.yml?style=for-the-badge&label=Build&labelColor=black)
![Tests](https://img.shields.io/github/actions/workflow/status/sh1ngekyo/blog-template/tests.yml?style=for-the-badge&label=Tests&labelColor=black)

This template can be used to create a blog/forum based on Asp.net core (API or MVC). Currently presentation layer implemented with MVC.

# Features:
1. Role based Auth
2. Admin panel (Create, update, delete posts for Authors, manage users, site settings, pages, etc for Admins)
3. Notifications
4. Smart pagination
5. Nested comments for posts
6. User profiles

# Improvements:
1. Move front-end to React/Angular/Vue
2. Implement tags & search for posts
3. Move from localdb to postgresql/mssql server

# Packages:
1. EF Core
2. MediatR
4. AutoMapper
5. FluentValidation
6. ToastNotification
7. X.PagedList
8. NSubstitude (Tests)

# Download & setup:
1. Download/Clone: `git clone https://github.com/sh1ngekyo/blog-template.git`
2. Apply Migration in your package manager: `Update-Database` or use dotnet cli.
3. Run & configure your site in Dashboard. Default Login: Admin, Default Password: Admin@0011. Change default account settings in DbInitializer.

# Blog example/Features:

## Home Page
<details> 
  <summary><h3>Top first page</h3></summary>
  <img src="https://github.com/sh1ngekyo/blog-template/blob/master/Docs/Images/HomeFirstPage.png">
</details>
<details> 
  <summary><h3>Bottom first page</h3></summary>
  <img src="https://github.com/sh1ngekyo/blog-template/blob/master/Docs/Images/HomeBottomPage.png">
</details>

## SignIn/SignUp
<details> 
  <summary><h3>SignIn</h3></summary>
  <img src="https://github.com/sh1ngekyo/blog-template/blob/master/Docs/Images/Login.png">
</details>
<details> 
  <summary><h3>SignUp</h3></summary>
  <img src="https://github.com/sh1ngekyo/blog-template/blob/master/Docs/Images/Register.png">
</details>

## Dashboard Differences
<details> 
  <summary><h3>Admin</h3></summary>
  <img src="https://github.com/sh1ngekyo/blog-template/blob/master/Docs/Images/AdminDashboard.png">
</details>
<details> 
  <summary><h3>Author</h3></summary>
  <img src="https://github.com/sh1ngekyo/blog-template/blob/master/Docs/Images/AuthorDashboard.png">
</details>

## Dashboard Posts
<details> 
  <summary><h3>Add Post</h3></summary>
  <img src="https://github.com/sh1ngekyo/blog-template/blob/master/Docs/Images/AddPost.png">
</details>
<details> 
  <summary><h3>Edit Post</h3></summary>
  <img src="https://github.com/sh1ngekyo/blog-template/blob/master/Docs/Images/EditPost.png">
</details>

## Dashboard Options
<details> 
  <summary><h3>Users</h3></summary>
  <img src="https://github.com/sh1ngekyo/blog-template/blob/master/Docs/Images/Users.png">
</details>
<details> 
  <summary><h3>Settings</h3></summary>
  <img src="https://github.com/sh1ngekyo/blog-template/blob/master/Docs/Images/Settings.png">
</details>
<details> 
  <summary><h3>Page Edit</h3></summary>
  <img src="https://github.com/sh1ngekyo/blog-template/blob/master/Docs/Images/PagesEdit.png">
</details>

## Profile
<details> 
  <summary><h3>Edit</h3></summary>
  <img src="https://github.com/sh1ngekyo/blog-template/blob/master/Docs/Images/ProfileEdit.png">
</details>
<details> 
  <summary><h3>Public View</h3></summary>
  <img src="https://github.com/sh1ngekyo/blog-template/blob/master/Docs/Images/PublicProfile.png">
</details>

## Comments
<details> 
  <summary><h3>View from user with Author role</h3></summary>
  <img src="https://github.com/sh1ngekyo/blog-template/blob/master/Docs/Images/Comments.png">
</details>
