class University {
    constructor(data = {}) {
        this._universityId = data.universityId || 0;
        this._universityName = data.universityName || '';
        this._universityNameInKhmer = data.universityNameInKhmer || '';
        this._abbreviationName = data.abbreviationName || '';
    }

    // Getter and setter for universityId
    get universityId() {
        return this._universityId;
    }

    set universityId(value) {
        this._universityId = value;
    }

    // Getter and setter for universityName
    get universityName() {
        return this._universityName;
    }

    set universityName(value) {
        this._universityName = value;
    }

    // Getter and setter for universityNameInKhmer
    get universityNameInKhmer() {
        return this._universityNameInKhmer;
    }

    set universityNameInKhmer(value) {
        this._universityNameInKhmer = value;
    }

    // Getter and setter for abbreviationName
    get abbreviationName() {
        return this._abbreviationName;
    }

    set abbreviationName(value) {
        this._abbreviationName = value;
    }

    static fromApiArray(jsonArray) {
        return jsonArray.map(item => new University(item));
    }
}
