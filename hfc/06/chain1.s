	.file	"chain.c"
	.intel_syntax noprefix
	.def	__main;	.scl	2;	.type	32;	.endef
	.section .rdata,"dr"
.LC0:
	.ascii "taiwan\0"
.LC1:
	.ascii "09:00\0"
.LC2:
	.ascii "17:00\0"
.LC3:
	.ascii "hainan\0"
.LC4:
	.ascii "10:00\0"
.LC5:
	.ascii "18:00\0"
.LC6:
	.ascii "shangchuan\0"
.LC7:
	.ascii "08:00\0"
.LC8:
	.ascii "xiachuan\0"
	.align 8
.LC9:
	.ascii "%s opens at %s, closes at %s.\12\0"
	.text
	.globl	main
	.def	main;	.scl	2;	.type	32;	.endef
	.seh_proc	main
main:
	push	rbp
	.seh_pushreg	rbp
	mov	rbp, rsp
	.seh_setframe	rbp, 0
	sub	rsp, 176
	.seh_stackalloc	176
	.seh_endprologue
	call	__main
	lea	rax, .LC0[rip]
	mov	QWORD PTR -48[rbp], rax
	lea	rax, .LC1[rip]
	mov	QWORD PTR -40[rbp], rax
	lea	rax, .LC2[rip]
	mov	QWORD PTR -32[rbp], rax
	mov	QWORD PTR -24[rbp], 0
	lea	rax, .LC3[rip]
	mov	QWORD PTR -80[rbp], rax
	lea	rax, .LC4[rip]
	mov	QWORD PTR -72[rbp], rax
	lea	rax, .LC5[rip]
	mov	QWORD PTR -64[rbp], rax
	mov	QWORD PTR -56[rbp], 0
	lea	rax, .LC6[rip]
	mov	QWORD PTR -112[rbp], rax
	lea	rax, .LC7[rip]
	mov	QWORD PTR -104[rbp], rax
	lea	rax, .LC5[rip]
	mov	QWORD PTR -96[rbp], rax
	mov	QWORD PTR -88[rbp], 0
	lea	rax, .LC8[rip]
	mov	QWORD PTR -144[rbp], rax
	lea	rax, .LC7[rip]
	mov	QWORD PTR -136[rbp], rax
	lea	rax, .LC5[rip]
	mov	QWORD PTR -128[rbp], rax
	mov	QWORD PTR -120[rbp], 0
	lea	rax, -80[rbp]
	mov	QWORD PTR -24[rbp], rax
	lea	rax, -112[rbp]
	mov	QWORD PTR -56[rbp], rax
	lea	rax, -48[rbp]
	mov	QWORD PTR -8[rbp], rax
	jmp	.L2
.L3:
	mov	rax, QWORD PTR -8[rbp]
	mov	rcx, QWORD PTR 16[rax]
	mov	rax, QWORD PTR -8[rbp]
	mov	rdx, QWORD PTR 8[rax]
	mov	rax, QWORD PTR -8[rbp]
	mov	rax, QWORD PTR [rax]
	mov	r9, rcx
	mov	r8, rdx
	mov	rdx, rax
	lea	rcx, .LC9[rip]
	call	printf
	mov	rax, QWORD PTR -8[rbp]
	mov	rax, QWORD PTR 24[rax]
	mov	QWORD PTR -8[rbp], rax
.L2:
	cmp	QWORD PTR -8[rbp], 0
	jne	.L3
	lea	rax, -144[rbp]
	mov	QWORD PTR -24[rbp], rax
	lea	rax, -112[rbp]
	mov	QWORD PTR -120[rbp], rax
	lea	rax, -48[rbp]
	mov	QWORD PTR -8[rbp], rax
	jmp	.L4
.L5:
	mov	rax, QWORD PTR -8[rbp]
	mov	rcx, QWORD PTR 16[rax]
	mov	rax, QWORD PTR -8[rbp]
	mov	rdx, QWORD PTR 8[rax]
	mov	rax, QWORD PTR -8[rbp]
	mov	rax, QWORD PTR [rax]
	mov	r9, rcx
	mov	r8, rdx
	mov	rdx, rax
	lea	rcx, .LC9[rip]
	call	printf
	mov	rax, QWORD PTR -8[rbp]
	mov	rax, QWORD PTR 24[rax]
	mov	QWORD PTR -8[rbp], rax
.L4:
	cmp	QWORD PTR -8[rbp], 0
	jne	.L5
	mov	eax, 0
	add	rsp, 176
	pop	rbp
	ret
	.seh_endproc
	.ident	"GCC: (GNU) 4.8.3"
	.def	printf;	.scl	2;	.type	32;	.endef
