class Product {
    constructor({
                    productId = null,
                    productName = null,
                    productNameInKhmer = null,
                    description = null,
                    vat = null,
                    price = null,
                    type = null,
                    status = null,
                    tuitionFees = null,
                    degreeId = null,
                    orderId = null,
                    cardCertificate = null,
                    categoryId = null,
                    priceKhr = null,
                    paymentType = null,
                    fromPromotion = null,
                    toPromotion = null,
                    hidden = null
                } = {}) {
        this._productId = productId;
        this._productName = productName;
        this._productNameInKhmer = productNameInKhmer;
        this._description = description;
        this._vat = vat;
        this._price = price;
        this._type = type;
        this._status = status;
        this._tuitionFees = tuitionFees;
        this._degreeId = degreeId;
        this._orderId = orderId;
        this._cardCertificate = cardCertificate;
        this._categoryId = categoryId;
        this._priceKhr = priceKhr;
        this._paymentType = paymentType || "";
        this._fromPromotion = fromPromotion;
        this._toPromotion = toPromotion;
        this._hidden = hidden;
    }

    // ProductId
    get productId() {
        return this._productId;
    }

    set productId(value) {
        this._productId = value;
    }

    // ProductName
    get productName() {
        return this._productName;
    }

    set productName(value) {
        this._productName = value;
    }

    // ProductNameInKhmer
    get productNameInKhmer() {
        return this._productNameInKhmer;
    }

    set productNameInKhmer(value) {
        this._productNameInKhmer = value;
    }

    // Description
    get description() {
        return this._description;
    }

    set description(value) {
        this._description = value;
    }

    // VAT
    get vat() {
        return this._vat;
    }

    set vat(value) {
        this._vat = value;
    }

    // Price
    get price() {
        return this._price;
    }

    set price(value) {
        this._price = value;
    }

    // Type
    get type() {
        return this._type;
    }

    set type(value) {
        this._type = value;
    }

    // Status
    get status() {
        return this._status;
    }

    set status(value) {
        this._status = value;
    }

    // TuitionFees
    get tuitionFees() {
        return this._tuitionFees;
    }

    set tuitionFees(value) {
        this._tuitionFees = value;
    }

    // DegreeId
    get degreeId() {
        return this._degreeId;
    }

    set degreeId(value) {
        this._degreeId = value;
    }

    // OrderId
    get orderId() {
        return this._orderId;
    }

    set orderId(value) {
        this._orderId = value;
    }

    // CardCertificate
    get cardCertificate() {
        return this._cardCertificate;
    }

    set cardCertificate(value) {
        this._cardCertificate = value;
    }

    // CategoryId
    get categoryId() {
        return this._categoryId;
    }

    set categoryId(value) {
        this._categoryId = value;
    }

    // PriceKhr
    get priceKhr() {
        return this._priceKhr;
    }

    set priceKhr(value) {
        this._priceKhr = value;
    }

    // PaymentType
    get paymentType() {
        return this._paymentType;
    }

    set paymentType(value) {
        this._paymentType = value;
    }

    // FromPromotion
    get fromPromotion() {
        return this._fromPromotion;
    }

    set fromPromotion(value) {
        this._fromPromotion = value;
    }

    // ToPromotion
    get toPromotion() {
        return this._toPromotion;
    }

    set toPromotion(value) {
        this._toPromotion = value;
    }

    // Hidden
    get hidden() {
        return this._hidden;
    }

    set hidden(value) {
        this._hidden = value;
    }


    static fromApiArray(apiArray) {
        if (!Array.isArray(apiArray)) return [];
        return apiArray.map(item => new Product(item));
    }
}
