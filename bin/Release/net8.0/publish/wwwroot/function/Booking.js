class Booking {
    constructor({
                    bookingId = null,
                    bookingDate = null,
                    userId = null,
                    studentId = "",
                    exchangeId = null,
                    total = null,
                    vat = null,
                    discount = null,
                    payDollar = null,
                    payRieal = null,
                    note = "",
                    active = null,
                    degree = "",
                    schoolId = null,
                    fieldId = null,
                    promotionNo = null,
                    stageNo = null,
                    groupId = null,
                    termNo = null,
                    fromDate = null,
                    toDate = null,
                    studyTime = "",
                    updateBy = "",
                    updateDate = null,
                    returnAlready = null,
                    returnRateIn = null,
                    returnDate = null,
                    returnAmount = null,
                    returnDollar = null,
                    returnRiel = null,
                    bookingNo = null,
                    yearNumber = ""
                } = {}) {
        this.bookingId = bookingId;
        this.bookingDate = bookingDate ? new Date(bookingDate) : null;
        this.userId = userId;
        this.studentId = studentId;
        this.exchangeId = exchangeId;
        this.total = total;
        this.vat = vat;
        this.discount = discount;
        this.payDollar = payDollar;
        this.payRieal = payRieal;
        this.note = note;
        this.active = active === null ? null : Boolean(active);
        this.degree = degree;
        this.schoolId = schoolId;
        this.fieldId = fieldId;
        this.promotionNo = promotionNo;
        this.stageNo = stageNo;
        this.groupId = groupId;
        this.termNo = termNo;
        this.fromDate = fromDate ? new Date(fromDate) : null;
        this.toDate = toDate ? new Date(toDate) : null;
        this.studyTime = studyTime;
        this.updateBy = updateBy;
        this.updateDate = updateDate ? new Date(updateDate) : null;
        this.returnAlready = returnAlready === null ? null : Boolean(returnAlready);
        this.returnRateIn = returnRateIn;
        this.returnDate = returnDate ? new Date(returnDate) : null;
        this.returnAmount = returnAmount;
        this.returnDollar = returnDollar;
        this.returnRiel = returnRiel;
        this.bookingNo = bookingNo;
        this.yearNumber = yearNumber;
    }

    // --- Getters & Setters ---
    get id() {
        return this.bookingId;
    }

    set id(value) {
        this.bookingId = value;
    }

    get date() {
        return this.bookingDate;
    }

    set date(value) {
        this.bookingDate = value ? new Date(value) : null;
    }

    get user() {
        return this.userId;
    }

    set user(value) {
        this.userId = value;
    }

    get student() {
        return this.studentId;
    }

    set student(value) {
        this.studentId = value;
    }

    get exchange() {
        return this.exchangeId;
    }

    set exchange(value) {
        this.exchangeId = value;
    }

    get totalAmount() {
        return this.total;
    }

    set totalAmount(value) {
        this.total = value;
    }

    get vatRate() {
        return this.vat;
    }

    set vatRate(value) {
        this.vat = value;
    }

    get discountAmount() {
        return this.discount;
    }

    set discountAmount(value) {
        this.discount = value;
    }

    get payUSD() {
        return this.payDollar;
    }

    set payUSD(value) {
        this.payDollar = value;
    }

    get payKHR() {
        return this.payRieal;
    }

    set payKHR(value) {
        this.payRieal = value;
    }

    get bookingNote() {
        return this.note;
    }

    set bookingNote(value) {
        this.note = value;
    }

    get isActive() {
        return this.active;
    }

    set isActive(value) {
        this.active = Boolean(value);
    }

    get degreeName() {
        return this.degree;
    }

    set degreeName(value) {
        this.degree = value;
    }

    get school() {
        return this.schoolId;
    }

    set school(value) {
        this.schoolId = value;
    }

    get field() {
        return this.fieldId;
    }

    set field(value) {
        this.fieldId = value;
    }

    get promotionNumber() {
        return this.promotionNo;
    }

    set promotionNumber(value) {
        this.promotionNo = value;
    }

    get stageNumber() {
        return this.stageNo;
    }

    set stageNumber(value) {
        this.stageNo = value;
    }

    get group() {
        return this.groupId;
    }

    set group(value) {
        this.groupId = value;
    }

    get termNumber() {
        return this.termNo;
    }

    set termNumber(value) {
        this.termNo = value;
    }

    get from() {
        return this.fromDate;
    }

    set from(value) {
        this.fromDate = value ? new Date(value) : null;
    }

    get to() {
        return this.toDate;
    }

    set to(value) {
        this.toDate = value ? new Date(value) : null;
    }

    get studyTimeText() {
        return this.studyTime;
    }

    set studyTimeText(value) {
        this.studyTime = value;
    }

    get updatedBy() {
        return this.updateBy;
    }

    set updatedBy(value) {
        this.updateBy = value;
    }

    get updatedDate() {
        return this.updateDate;
    }

    set updatedDate(value) {
        this.updateDate = value ? new Date(value) : null;
    }

    get isReturned() {
        return this.returnAlready;
    }

    set isReturned(value) {
        this.returnAlready = Boolean(value);
    }

    get returnRate() {
        return this.returnRateIn;
    }

    set returnRate(value) {
        this.returnRateIn = value;
    }

    get returnOn() {
        return this.returnDate;
    }

    set returnOn(value) {
        this.returnDate = value ? new Date(value) : null;
    }

    get returnAmountValue() {
        return this.returnAmount;
    }

    set returnAmountValue(value) {
        this.returnAmount = value;
    }

    get returnUSD() {
        return this.returnDollar;
    }

    set returnUSD(value) {
        this.returnDollar = value;
    }

    get returnKHR() {
        return this.returnRiel;
    }

    set returnKHR(value) {
        this.returnRiel = value;
    }

    get bookingNumber() {
        return this.bookingNo;
    }

    set bookingNumber(value) {
        this.bookingNo = value;
    }

    get year() {
        return this.yearNumber;
    }

    set year(value) {
        this.yearNumber = value;
    }
}
