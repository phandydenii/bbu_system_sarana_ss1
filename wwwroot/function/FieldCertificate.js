class FieldCertificate {
    constructor(data = {}) {
        this._id = data.id || 0;
        this._degreeId = data.degreeId || 0;
        this._degreeName = data.degreeName || '';
        this._degreeNameKhmer = data.degreeNameKhmer || '';
        this._schoolId = data.schoolId || 0;
        this._schoolName = data.schoolName || '';
        this._schoolNameKhmer = data.schoolNameKhmer || '';
        this._fieldId = data.fieldId || 0;
        this._fieldName = data.fieldName || '';
        this._fieldNameKhmer = data.fieldNameKhmer || '';
        this._promotionNo = data.promotionNo || 0;
        this._status = data.status ?? false; // bit -> boolean
        this._type = data.type || '';
        this._typeKhmer = data.typeKhmer || '';
    }

    // =========================
    // Getters and Setters
    // =========================
    get id() {
        return this._id;
    }

    set id(value) {
        this._id = Number(value) || 0;
    }

    get degreeId() {
        return this._degreeId;
    }

    set degreeId(value) {
        this._degreeId = Number(value) || 0;
    }

    get degreeName() {
        return this._degreeName;
    }

    set degreeName(value) {
        this._degreeName = value?.trim() || '';
    }

    get degreeNameKhmer() {
        return this._degreeNameKhmer;
    }

    set degreeNameKhmer(value) {
        this._degreeNameKhmer = value?.trim() || '';
    }

    get schoolId() {
        return this._schoolId;
    }

    set schoolId(value) {
        this._schoolId = Number(value) || 0;
    }

    get schoolName() {
        return this._schoolName;
    }

    set schoolName(value) {
        this._schoolName = value?.trim() || '';
    }

    get schoolNameKhmer() {
        return this._schoolNameKhmer;
    }

    set schoolNameKhmer(value) {
        this._schoolNameKhmer = value?.trim() || '';
    }

    get fieldId() {
        return this._fieldId;
    }

    set fieldId(value) {
        this._fieldId = Number(value) || 0;
    }

    get fieldName() {
        return this._fieldName;
    }

    set fieldName(value) {
        this._fieldName = value?.trim() || '';
    }

    get fieldNameKhmer() {
        return this._fieldNameKhmer;
    }

    set fieldNameKhmer(value) {
        this._fieldNameKhmer = value?.trim() || '';
    }

    get promotionNo() {
        return this._promotionNo;
    }

    set promotionNo(value) {
        this._promotionNo = Number(value) || 0;
    }

    get status() {
        return this._status;
    }

    set status(value) {
        this._status = Boolean(value);
    }

    get type() {
        return this._type;
    }

    set type(value) {
        this._type = value?.trim() || '';
    }

    get typeKhmer() {
        return this._typeKhmer;
    }

    set typeKhmer(value) {
        this._typeKhmer = value?.trim() || '';
    }

    // =========================
    // Computed property example
    // =========================
    get fullDegreeName() {
        return `${this.degreeName} (${this.degreeNameKhmer})`;
    }

    get fullSchoolName() {
        return `${this.schoolName} (${this.schoolNameKhmer})`;
    }

    get fullFieldName() {
        return `${this.fieldName} (${this.fieldNameKhmer})`;
    }

    // =========================
    // Static helper: map array of objects
    // =========================
    static fromApiArray(jsonArray) {
        return jsonArray.map(item => new FieldCertificate(item));
    }
}
