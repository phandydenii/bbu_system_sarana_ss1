const requestName = "letterCategory";
export const TABLE_CONFIG = {
    COLUMNS: {
        ID: "CategoryId",
        NAME: "CategoryName",
        PRICE: "UnitPrice",
        IS_ADMIN: "IsAdmin",
        IS_FOUNDATION: "IsFoundation",
        IS_SHORT_COURSE: "IsShortCourse",
        ACTIVE: "Active"
    },
    URLS: {
        GET_LETTER_CATEGORY_LIST: "/LetterCategory/get-letter-category",
    },

    IDS: {
        ID: `${requestName}_categoryid`,
        NAME: `${requestName}_categoryname`,
        PRICE: `${requestName}_unitprice`,
        IS_ADMIN: `${requestName}_isadmin`,
        IS_FOUNDATION: `${requestName}_isfoundation`,
        IS_SHORT_COURSE: `${requestName}_isshortcourse`,
        ACTIVE: `${requestName}_active`
    }
};

