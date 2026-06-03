class Group {
    constructor({
                    groupId = null,
                    groupName = "",
                    studyTime = "",
                    stageId = null,
                    fieldId = null,
                    createdInTermNo = null,
                    note = ""
                } = {}) {
        this.groupId = groupId;
        this.groupName = groupName;
        this.studyTime = studyTime;
        this.stageId = stageId;
        this.fieldId = fieldId;
        this.createdInTermNo = createdInTermNo;
        this.note = note;
    }

    // --- Getters & Setters ---

    get id() {
        return this.groupId;
    }

    set id(value) {
        this.groupId = value;
    }

    get name() {
        return this.groupName;
    }

    set name(value) {
        this.groupName = value;
    }

    get time() {
        return this.studyTime;
    }

    set time(value) {
        this.studyTime = value;
    }

    get stage() {
        return this.stageId;
    }

    set stage(value) {
        this.stageId = value;
    }

    get field() {
        return this.fieldId;
    }

    set field(value) {
        this.fieldId = value;
    }

    get createdTermNo() {
        return this.createdInTermNo;
    }

    set createdTermNo(value) {
        this.createdInTermNo = value;
    }

    get noteText() {
        return this.note;
    }

    set noteText(value) {
        this.note = value;
    }

    static fromApiArray(jsonArray) {
        return jsonArray.map(item => new Group(item));
    }

    static async GetGroups({stageId=0, fieldId=0}) {
        try {
            const response = await $.ajax({
                url: "/group/get-groups",
                method: 'POST',
                data: {
                    isAll: true,
                    stageId,
                    fieldId
                }
            });
            if (response.status.code === "200" && response.data !== "") {
                return Group.fromApiArray(response.data);
            } else {
                ShowToastError(response.message);
            }
        } catch (err) {
            ShowToastError(err);
            return [];
        }
    }
}
