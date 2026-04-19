class Field {
    constructor({
                    fieldId = null,
                    fieldName = "",
                    fieldNameInKhmer = "",
                    schoolId = null,
                    degreeId = null,
                    degreeName = "",
                    degreeNameInKhmer = "",
                    type = null
                } = {}) {
        this.fieldId = fieldId;
        this.fieldName = fieldName;
        this.fieldNameInKhmer = fieldNameInKhmer;
        this.schoolId = schoolId;
        this.degreeId = degreeId;
        this.degreeName = degreeName;
        this.degreeNameInKhmer = degreeNameInKhmer;
        this.type = type; // boolean
    }

    // --- Getters & Setters ---

    get id() {
        return this.fieldId;
    }

    set id(value) {
        this.fieldId = value;
    }

    get name() {
        return this.fieldName;
    }

    set name(value) {
        this.fieldName = value;
    }

    get nameKhmer() {
        return this.fieldNameInKhmer;
    }

    set nameKhmer(value) {
        this.fieldNameInKhmer = value;
    }

    get school() {
        return this.schoolId;
    }

    set school(value) {
        this.schoolId = value;
    }

    get degree() {
        return this.degreeId;
    }

    set degree(value) {
        this.degreeId = value;
    }

    get degreeTitle() {
        return this.degreeName;
    }

    set degreeTitle(value) {
        this.degreeName = value;
    }

    get degreeTitleKhmer() {
        return this.degreeNameInKhmer;
    }

    set degreeTitleKhmer(value) {
        this.degreeNameInKhmer = value;
    }

    get fieldType() {
        return this.type;
    }

    set fieldType(value) {
        this.type = Boolean(value);
    }

    static fromApiArray(jsonArray) {
        return jsonArray.map(item => new Field(item));
    }

    static async GetFields(param = {}) {
        try {
            const response = await $.ajax({
                url: "/field/get-fields",
                method: 'POST',
                data: {
                    isAll: param.isAll,
                    schoolId: param.schoolId,
                    degreeId: param.degreeId,
                },
                dataType: "json",
            });
            if (response.status.code === "200" && response.data !== "") {
                return Field.fromApiArray(response.data);
            } else {
                ShowToastError(response.message);
            }
        } catch (err) {
            ShowToastError(err);
            return [];
        }
    }
}
