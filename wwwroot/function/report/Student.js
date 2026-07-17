const StudentMoeys = {
    Init: async function () {
        const root = $("#studentMoeysTab");
        if (!root.length) return;

        root.find(".select2").select2({
            width: "100%",
            placeholder: "Select an option",
            allowClear: true
        });

        this.BindEvents();

        await this.GetDegree();
        await this.GetSchool();
    },

    BindEvents: function () {
        const self = this;
        const root = $("#studentMoeysTab");

        root.off(".studentMoeys");

        root.on("change.studentMoeys", "#cboDegree", async function () {
            await self.GetField();
            await self.GetPromotion();
            self.ClearSelect("#cboStage", "Select Stage");
            self.ClearSelect("#cboTerm", "Select Term");
            self.ClearSelect("#cboGroup", "Select Group");
        });

        root.on("change.studentMoeys", "#cboSchool", async function () {
            await self.GetField();
            await self.GetPromotion();
            self.ClearSelect("#cboStage", "Select Stage");
            self.ClearSelect("#cboTerm", "Select Term");
            self.ClearSelect("#cboGroup", "Select Group");
        });

        root.on("change.studentMoeys", "#cboField", async function () {
            await self.GetGroup();
        });

        root.on("change.studentMoeys", "#cboPromotion", async function () {
            await self.GetStage();
            self.ClearSelect("#cboTerm", "Select Term");
            self.ClearSelect("#cboGroup", "Select Group");
        });

        root.on("change.studentMoeys", "#cboStage", async function () {
            await self.GetTerm();
            await self.GetGroup();
        });

        root.on("click.studentMoeys", "#btnPrintMoeys", function () {
            self.Print();
        });
    },

    GetDegree: async function () {
        const cboDegree = $("#studentMoeysTab #cboDegree");
        cboDegree.empty().append("<option value=''>Select Degree</option>");

        try {
            console.log("Calling degree API");

            const degrees = await Degree.GetAllDegree();

            degrees.forEach(function (item) {
                cboDegree.append(`<option value="${item.degreeId}">${item.degreeName}</option>`);
            });

            cboDegree.val("").trigger("change.select2");
        } catch (error) {
            console.error("Degree API error:", error);
        }
    },

    GetSchool: async function () {
        const cboSchool = $("#studentMoeysTab #cboSchool");
        cboSchool.empty().append("<option value=''>Select School</option>");

        try {
            console.log("Calling school API");

            const schools = await School.GetSchool();

            schools.forEach(function (item) {
                cboSchool.append(`<option value="${item.schoolId}">${item.schoolName}</option>`);
            });

            cboSchool.val("").trigger("change.select2");
        } catch (error) {
            console.error("School API error:", error);
        }
    },

    GetField: async function () {
        const degreeId = parseInt($("#studentMoeysTab #cboDegree").val());
        const schoolId = parseInt($("#studentMoeysTab #cboSchool").val());
        const cboField = $("#studentMoeysTab #cboField");

        cboField.empty().append("<option value=''>Select Field</option>");

        if (!degreeId || !schoolId) {
            cboField.val("").trigger("change.select2");
            return;
        }

        try {
            const fields = await Field.GetFields({isAll: true, degreeId, schoolId});

            fields.forEach(function (item) {
                cboField.append(`<option value="${item.fieldId}">${item.fieldName}</option>`);
            });

            cboField.val("").trigger("change.select2");
        } catch (error) {
            console.error("Field API error:", error);
        }
    },

    GetPromotion: async function () {
        const degreeId = parseInt($("#studentMoeysTab #cboDegree").val());
        const schoolId = parseInt($("#studentMoeysTab #cboSchool").val());
        const cboPromotion = $("#studentMoeysTab #cboPromotion");

        cboPromotion.empty().append("<option value=''>Select Promotion</option>");

        if (!degreeId || !schoolId) {
            cboPromotion.val("").trigger("change.select2");
            return;
        }

        try {
            const promotions = await Promotion.GetPromotions({schoolId, degreeId});

            promotions.forEach(function (item) {
                cboPromotion.append(`<option value="${item.promotionId}">${item.promotionNo}</option>`);
            });

            cboPromotion.val("").trigger("change.select2");
        } catch (error) {
            console.error("Promotion API error:", error);
        }
    },

    GetStage: async function () {
        const promotionId = parseInt($("#studentMoeysTab #cboPromotion").val());
        const cboStage = $("#studentMoeysTab #cboStage");

        cboStage.empty().append("<option value=''>Select Stage</option>");

        if (!promotionId) {
            cboStage.val("").trigger("change.select2");
            return;
        }

        try {
            const stages = await Stage.GetStages({promotionId});

            stages.forEach(function (item) {
                cboStage.append(`<option value="${item.stageId}">${item.stageNo}</option>`);
            });

            cboStage.val("").trigger("change.select2");
        } catch (error) {
            console.error("Stage API error:", error);
        }
    },

    GetTerm: async function () {
        const stageId = parseInt($("#studentMoeysTab #cboStage").val());
        const cboTerm = $("#studentMoeysTab #cboTerm");

        cboTerm.empty().append("<option value=''>Select Term</option>");

        if (!stageId) {
            cboTerm.val("").trigger("change.select2");
            return;
        }

        try {
            const terms = await Term.GetTerms({stageId});

            terms.forEach(function (item) {
                cboTerm.append(`<option value="${item.termNo}">${item.termNo}</option>`);
            });

            cboTerm.val("").trigger("change.select2");
        } catch (error) {
            console.error("Term API error:", error);
        }
    },

    GetGroup: async function () {
        const stageId = parseInt($("#studentMoeysTab #cboStage").val());
        const fieldId = parseInt($("#studentMoeysTab #cboField").val());
        const cboGroup = $("#studentMoeysTab #cboGroup");

        cboGroup.empty().append("<option value=''>Select Group</option>");

        if (!stageId || !fieldId) {
            cboGroup.val("").trigger("change.select2");
            return;
        }

        try {
            const groups = await Group.GetGroups({stageId, fieldId});

            groups.forEach(function (item) {
                cboGroup.append(`<option value="${item.groupId}">${item.groupName}</option>`);
            });

            cboGroup.val("").trigger("change.select2");
        } catch (error) {
            console.error("Group API error:", error);
        }
    },

    ClearSelect: function (selector, text) {
        const select = $("#studentMoeysTab").find(selector);
        select.empty().append(`<option value="">${text}</option>`);
        select.val("").trigger("change.select2");
    },

    Print: function () {
        const degreeId = $("#studentMoeysTab #cboDegree").val();
        const schoolId = $("#studentMoeysTab #cboSchool").val();
        const fieldId = $("#studentMoeysTab #cboField").val();
        const promotionId = $("#studentMoeysTab #cboPromotion").val();
        const filter = $("#studentMoeysTab #cboFilter").val();

        if (!degreeId || !schoolId || !fieldId || !promotionId) {
            alert("Please select Degree, School, Field and Promotion.");
            return;
        }

        showLoading();

        $.ajax({
            url: "/report/student-moeys",
            type: "POST",
            data: {degreeId, schoolId, fieldId, promotionId, filter},
            xhrFields: {
                responseType: "blob"
            },
            success: function (blob) {
                if (window.studentMoeysPdfUrl) URL.revokeObjectURL(window.studentMoeysPdfUrl);

                window.studentMoeysPdfUrl = URL.createObjectURL(blob);
                $("#studentMoeysTab #reportContainer").attr("src", window.studentMoeysPdfUrl);
            },
            error: function (xhr) {
                console.error("Report error:", xhr);
                alert("Unable to generate report.");
            },
            complete: function () {
                hideLoading(1);
            }
        });
    }
};