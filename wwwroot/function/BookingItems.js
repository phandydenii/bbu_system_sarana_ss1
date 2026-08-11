
class BookingItems {
    constructor({
        bookingItemId = null,
        itemName = null,
        itemNameKhmer = null,
        price = null,
                } = {}) {
        this.bookingItemId = bookingItemId;
        this.itemName = itemName;
        this.itemNameKhmer = itemNameKhmer;
        this.price = price;
    }
    // --- Getters & Setters ---
    get id() {
        return this.bookingItemId;
    }
    set id(value) {
        this.bookingItemId = value;
    }
    get name() {
        return this.itemName;
    }
    set name(value) {
        this.itemName = value;
    }
    get nameKhmer() {
        return this.itemNameKhmer;
    }
    set nameKhmer(value) {
        this.itemNameKhmer = value;
    }
    get itemprice() {
        return this.price;
    }
    set itemprice(value) {
        this.price = value;
    }
}