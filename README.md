# ProductManagementSys
My ASP.NET MVC course practice

這是我在「緯育」線上課程 ASP.NET MVC 學習的課程內容練習

主要為產品管理系統

其中包含:
1. 會員登入權限:

    建立 Model 從資料庫中的會員資料進行提取，確認登入者是否擁有會員帳號。

2. 類別管理:

    根據產品類型進行分類，並以 List 形式顯示在 View 上，同時根據每個會員的權限，在各個類別上可以做編輯或刪除的功能。
3. 類別新增:

    擁有「類別新增」權限的會員，可以在此頁面進行新增類別的動作，以 Create 形式顯示在 View 上。
4. 產品管理:

    在頁面的左側顯示類別選項，右側則是該類別的產品資料，包含產品編號、品名、單價......
  可以根據左側的的類別選項，選擇想要顯示的類別去查詢產品的內容，同時各個產品也可以進行
  編輯及刪除的功能。

5. 產品新增:

    在頁面上以 Create 形式將產品資料該新增的細項都顯示在 View 上面，同時也可以上傳產品
  圖示，以便使用者更直覺的了解此產品內容。

6. 會員管理:

    在會員管理頁面，僅有 admin 帳號擁有此權限，可以在頁面中管理各個會員帳號的權限，
  權限包含讀取、新增、修改、刪除功能，而每個會員都有最基本的讀取功能，其餘功能可在
  此頁面中進行編輯新增。

7. 會員新增:

    在會員新增頁面，僅有 admin 帳號擁有此權限，在此頁面可以新增會員帳號、密碼、角色及
  權限。

