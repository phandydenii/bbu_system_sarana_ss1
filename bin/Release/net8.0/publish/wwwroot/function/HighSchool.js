class HighSchool {
    constructor(data = {}) {
        this._highSchoolId = data.highSchoolId || 0;
        this._highSchoolName = data.highSchoolName || '';
        this._highSchoolNameInKhmer = data.highSchoolNameInKhmer || '';
    }

    // Getter and setter for highSchoolId
    get highSchoolId() {
        return this._highSchoolId;
    }

    set highSchoolId(value) {
        this._highSchoolId = value;
    }

    // Getter and setter for highSchoolName
    get highSchoolName() {
        return this._highSchoolName;
    }

    set highSchoolName(value) {
        this._highSchoolName = value;
    }

    // Getter and setter for highSchoolNameInKhmer
    get highSchoolNameInKhmer() {
        return this._highSchoolNameInKhmer;
    }

    set highSchoolNameInKhmer(value) {
        this._highSchoolNameInKhmer = value;
    }

    static fromApiArray(jsonArray) {
        return jsonArray.map(item => new HighSchool(item));
    }
}
