class LetterCategoryModel {
    constructor(data = {}) {
        this.categoryId = data.categoryId || data.categoryid || 0;
        this.categoryName = data.categoryName;
        this.unitPrice = data.unitPrice || 0;
        this.isAdmin = data.isAdmin || data.isadmin || false;
        this.isFoundation = data.isFoundation || data.isfoundation || false;
        this.isShortCourse = data.isShortCourse || data.isshortcourse || false;
        this.active = data.active ?? true;
    }

    // Convert API data to our model
    static fromApi(data) {
        return new LetterCategoryModel({
            categoryId: data.categoryid,
            categoryName: data.categoryName,
            unitPrice: data.unitPrice,
            isAdmin: data.isAdmin,
            isFoundation: data.isFoundation,
            isShortCourse: data.isShortCourse,
            active: data.active
        });
    }

    toApiFormat() {
        return {
            categoryid: this.categoryId,
            name: this.name,
            price: this.price,
            isadmin: this.isAdmin,
            isfoundation: this.isFoundation,
            isshortcourse: this.isShortCourse,
            active: this.active
        };
    }

    // Validation method
    isValid() {
        return Boolean(this.name && this.price >= 0);
    }

    // Get status text
    getStatusText() {
        return this.active ? 'Active' : 'Inactive';
    }
}

// Export the class
export {LetterCategoryModel};
